// Map Service for Leaflet
const MapService = {
    map: null,
    fbiMarkers: [],
    userMarker: null,
    updateInterval: null,
    tagDistance: 10, // meters - distance to be able to tag a target

    // Calculate distance between two coordinates in meters (Haversine formula)
    calculateDistance(lat1, lon1, lat2, lon2) {
        const R = 6371e3; // Earth's radius in meters
        const φ1 = lat1 * Math.PI / 180;
        const φ2 = lat2 * Math.PI / 180;
        const Δφ = (lat2 - lat1) * Math.PI / 180;
        const Δλ = (lon2 - lon1) * Math.PI / 180;

        const a = Math.sin(Δφ/2) * Math.sin(Δφ/2) +
                  Math.cos(φ1) * Math.cos(φ2) *
                  Math.sin(Δλ/2) * Math.sin(Δλ/2);
        const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));

        return R * c; // Distance in meters
    },

    async initMap(latitude, longitude) {
        console.log('=== MAP INIT START ===');
        console.log('Latitude:', latitude);
        console.log('Longitude:', longitude);
        
        // Use default Copenhagen coordinates if none provided
        const lat = latitude || 55.6761;
        const lon = longitude || 12.5683;
        
        console.log('Using coordinates:', lat, lon);
        
        // Clean up existing map
        if (this.map) {
            this.map.remove();
            this.map = null;
        }
        
        // Wait a moment for DOM
        setTimeout(async () => {
            console.log('Attempting to create map...');
            
            // Create map with zoom and dragging disabled
            this.map = L.map('map', {
                center: [lat, lon],
                zoom: 17,
                zoomControl: false,       // Remove zoom buttons
                scrollWheelZoom: false,   // Disable scroll wheel zoom
                doubleClickZoom: false,   // Disable double click zoom
                touchZoom: false,         // Disable pinch zoom on mobile
                boxZoom: false,           // Disable shift+drag zoom
                keyboard: false,          // Disable keyboard zoom
                dragging: false           // Disable dragging
            });
            
            // Add tiles
            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '© OpenStreetMap'
            }).addTo(this.map);
            
            // Add user location marker (blue)
            this.userMarker = L.marker([lat, lon], {
                icon: L.icon({
                    iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-blue.png',
                    shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.9.4/images/marker-shadow.png',
                    iconSize: [25, 41],
                    iconAnchor: [12, 41],
                    popupAnchor: [1, -34],
                    shadowSize: [41, 41]
                })
            }).addTo(this.map)
                .bindPopup('Your Location')
                .openPopup();
            
            // Fix map size after rendering
            setTimeout(() => {
                this.map.invalidateSize();
            }, 100);
            
            console.log('=== MAP INIT COMPLETE ===');
            
            // Load FBI most wanted targets
            await this.loadFBITargets(lat, lon);
            
            // Start live position updates
            this.startLiveTracking();
        }, 300);
    },

    startLiveTracking() {
        console.log('Starting live position tracking...');
        // Update position every 3 seconds
        this.updateInterval = setInterval(async () => {
            const currentPlayer = await GameService.getCurrentPlayer();
            if (currentPlayer?.currentUser) {
                const newLat = currentPlayer.currentUser.latitude;
                const newLon = currentPlayer.currentUser.longitude;
                
                // Update user marker position
                if (this.userMarker && newLat && newLon) {
                    this.userMarker.setLatLng([newLat, newLon]);
                    this.map.setView([newLat, newLon], 17);
                    console.log('Position updated:', newLat, newLon);
                    
                    // Check proximity to targets
                    this.checkProximity(newLat, newLon);
                }
            }
        }, 2000);
    },

    checkProximity(userLat, userLon) {
        this.fbiMarkers.forEach((markerData) => {
            const targetLatLng = markerData.marker.getLatLng();
            const distance = this.calculateDistance(
                userLat, userLon,
                targetLatLng.lat, targetLatLng.lng
            );
            
            markerData.distance = distance;
            markerData.inRange = distance <= this.tagDistance;
            
            // Change marker color if in range (green) vs out of range (red)
            if (markerData.inRange) {
                console.log(`Target "${markerData.target.title}" is IN RANGE! Distance: ${distance.toFixed(2)}m`);
                // You can change marker icon to green here
            }
        });
        
        // Emit event for Vue to update UI
        if (window.vueApp) {
            window.vueApp.updateTargetsInRange(this.fbiMarkers.filter(m => m.inRange));
        }
    },

    async loadFBITargets(userLat, userLon) {
        console.log('Loading FBI most wanted targets...');
        const fbiData = await FBIService.getMostWanted(1, 10);
        
        if (!fbiData || !fbiData.items) {
            console.error('No FBI data received');
            return;
        }

        console.log(`Loading ${fbiData.items.length} FBI targets`);
        
        // Add FBI targets as red markers around the user location
        fbiData.items.forEach((target, index) => {
            console.log(`Target ${index} full object:`, JSON.stringify(target, null, 2));
            console.log(`All properties:`, Object.keys(target));
            console.log(`MaxReward (lowercase):`, target.maxreward);
            console.log(`MaxReward (uppercase):`, target.maxReward);
            console.log(`MaxReward (PascalCase):`, target.MaxReward);
            
            // Generate random offset from user location (within 100 meters)
            // 1 degree of latitude = ~111,320 meters
            // 100 meters = ~0.0009 degrees
            const maxOffset = 0.0009;
            const latOffset = (Math.random() - 0.5) * 2 * maxOffset;
            const lonOffset = (Math.random() - 0.5) * 2 * maxOffset;
            
            const targetLat = userLat + latOffset;
            const targetLon = userLon + lonOffset;
            
            // Create red marker for FBI target
            const marker = L.marker([targetLat, targetLon], {
                icon: L.icon({
                    iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-red.png',
                    shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.9.4/images/marker-shadow.png',
                    iconSize: [25, 41],
                    iconAnchor: [12, 41],
                    popupAnchor: [1, -34],
                    shadowSize: [41, 41]
                })
            }).addTo(this.map);
            
            // Create popup with target info
            const popupContent = `
                <div style="text-align: center; min-width: 200px;">
                    <h6><strong>${target.title || 'Unknown'}</strong></h6>
                    ${target.images && target.images.length > 0 ? 
                        `<img src="${target.images[0].large}" style="width: 180px; height: auto; margin: 5px 0; border-radius: 5px;" />` : ''}
                    <p style="margin: 5px 0;"><strong>Reward:</strong> $${target.rewardMax ? target.rewardMax.toLocaleString() : '0'}</p>
                    <p style="margin: 5px 0; font-size: 11px;">${target.description ? target.description.substring(0, 100) + '...' : ''}</p>
                </div>
            `;
            
            marker.bindPopup(popupContent, {
                maxWidth: 250,
                minWidth: 200,
                minHeight: 600
            });
            
            // Store marker with target data
            this.fbiMarkers.push({
                marker: marker,
                target: target,
                distance: null,
                inRange: false
            });

            // Save villain to database
            const villainData = {
                title: target.title || 'Unknown',
                latitude: targetLat,
                longitude: targetLon,
                maxReward: target.rewardMax || 0
            };
            VillainService.saveVillain(villainData);
        });
        
        console.log(`Added ${this.fbiMarkers.length} FBI target markers`);
    },

    stopLiveTracking() {
        if (this.updateInterval) {
            clearInterval(this.updateInterval);
            this.updateInterval = null;
            console.log('Live tracking stopped');
        }
    },

    destroyMap() {
        this.stopLiveTracking();
        if (this.map) {
            this.map.remove();
            this.map = null;
            this.fbiMarkers = [];
            this.userMarker = null;
        }
    }
};

