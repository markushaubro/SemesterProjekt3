// Main Vue Application
const { createApp } = Vue;

const app = createApp({
  data() {
    return {
        currentView: 'start',
        profiles: [],
        newProfile: {
            name: "",
        },
        editingProfile: null,
        deleteProfileId: null,
        currentPlayer: null,
        targetsInRange: [],
    };
  },
  computed: {
    topFive() {
      return [...this.profiles]
        .sort((a, b) => b.score - a.score)
        .slice(0, 5);
    }
  },
  methods: {
        // UI Helper Methods
        getLeaderboardColor(index) {
            // Gradient from purple (#8B5CF6) to red (#DC2626)
            const colors = [
                '#8B5CF6', // Purple (rank 1)
                '#A855F7', // Purple-pink
                '#C026D3', // Magenta
                '#E11D48', // Pink-red
                '#DC2626'  // Red (rank 5)
            ];
            return colors[index] || '#6B7280'; // Gray fallback
        },

        // Navigation Methods
        startGame() {
            this.currentView = 'game';
            this.fetchProfiles();
        },
        goToStart() {
            this.currentView = 'start';
        },

        // Game Session Methods
        async startGameSession(playerId) {
            console.log('=== START GAME SESSION ===');
            const result = await GameService.startGameSession(playerId);
            
            if (result.success) {
                // Wait for current player to be set in API
                console.log('Waiting for current player data...');
                await new Promise(resolve => setTimeout(resolve, 500));
                
                this.currentPlayer = await GameService.getCurrentPlayer();
                console.log('Full CurrentPlayer object:', JSON.stringify(this.currentPlayer, null, 2));
                
                // Get coordinates from API (they are inside currentUser object)
                const lat = this.currentPlayer?.currentUser?.latitude;
                const lon = this.currentPlayer?.currentUser?.longitude;
                console.log('Extracted Latitude:', lat);
                console.log('Extracted Longitude:', lon);
                
                if (!lat || !lon) {
                    alert('Kunne ikke hente koordinater fra API. Tjek at latitude og longitude er sat.');
                    console.error('Missing coordinates!');
                    return;
                }
                
                // Switch to play view
                this.currentView = 'play';
                
                // Wait for Vue to render the map container
                await this.$nextTick();
                console.log('Vue rendered, initializing map...');
                
                // Wait a bit more for DOM
                await new Promise(resolve => setTimeout(resolve, 200));
                
                // Init map with API coordinates
                MapService.initMap(lat, lon);
            }
        },
        async endGameSession() {
            console.log('=== END GAME SESSION ===');
            MapService.destroyMap();
            
            // Delete all villains from database when game ends
            console.log('Calling VillainService.deleteAllVillains()...');
            const deleteResult = await VillainService.deleteAllVillains();
            console.log('Delete result:', deleteResult);
            
            const success = await GameService.endGameSession();
            if (success) {
                this.currentPlayer = null;
                this.currentView = 'game';
                await this.fetchProfiles();
            }
        },
        async addScore(points) {
            const success = await GameService.addScore(pointns);
            if (success) {
                this.currentPlayer = await GameService.getCurrentPlayer();
                await this.fetchProfiles();
            }
        },

        // Profile Management Methods
        async fetchProfiles() {
            this.profiles = await ProfileService.fetchProfiles();
        },
        async addProfile() {
            const success = await ProfileService.addProfile(this.newProfile.name);
            if (success) {
                this.newProfile.name = "";
                await this.fetchProfiles();
            }
        },
        async deleteProfile(id) {
            const success = await ProfileService.deleteProfile(id);
            if (success) {
                await this.fetchProfiles();
            }
        },
        editProfile(profile) {
            this.editingProfile = { ...profile };
        },
        cancelEdit() {
            this.editingProfile = null;
        },
        async updateProfile() {
            const success = await ProfileService.updateProfile(
                this.editingProfile.id,
                this.editingProfile.name,
                this.editingProfile.score
            );
            if (success) {
                this.editingProfile = null;
                await this.fetchProfiles();
            }
        },
        
        // Target tracking methods
        updateTargetsInRange(targets) {
            this.targetsInRange = targets;
        },
        
        async tagTarget(targetData) {
            // This will be connected to your backend later
            console.log('Tagging target:', targetData.target.title);
            alert(`Tagged: ${targetData.target.title}\nReward: $${targetData.target.rewardMax}\n\nBackend integration pending...`);
            
            // When backend is ready, call:
            // await TagService.tagTarget(targetData.target.id);
            // await GameService.addScore(targetData.target.rewardMax);
        },
    },
});

// Mount and expose to window for MapService
const vueAppInstance = app.mount("#app");
window.vueApp = vueAppInstance;
