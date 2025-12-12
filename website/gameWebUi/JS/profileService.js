// Profile API Service
const ProfileService = {
    async fetchProfiles() {
        try {
            const response = await fetch(API_CONFIG.baseUrl);
            if (response.ok) {
                return await response.json();
            } else {
                console.error("Failed to fetch profiles");
                return [];
            }
        } catch (error) {
            console.error("Error:", error);
            return [];
        }
    },

    async addProfile(name) {
        try {
            const response = await fetch(API_CONFIG.baseUrl, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    name: name,
                    score: 0,
                }),
            }); 
            if (response.ok) {
                return true;
            } else {
                console.error("Failed to add profile");
                return false;
            }
        } catch (error) {
            console.error("Error:", error);
            return false;
        }
    },

    async deleteProfile(id) {
        try {
            console.log('Deleting profile with ID:', id);
            const response = await fetch(`${API_CONFIG.baseUrl}/${id}`, {
                method: "DELETE",
            });
            console.log('Delete response status:', response.status);
            
            if (response.ok) {
                console.log('Profile deleted successfully');
                return true;
            } else {
                const errorText = await response.text();
                console.error("Failed to delete profile:", response.status, errorText);
                alert('Kunne ikke slette profilen: ' + response.status);
                return false;
            }
        } catch (error) {
            console.error("Error:", error);
            alert('Fejl ved sletning: ' + error.message);
            return false;
        }
    },

    async updateProfile(id, name, score) {
        try {
            const updateData = {
                name: name,
                score: score
            };
            
            console.log('Updating profile:', id, updateData);
            const response = await fetch(`${API_CONFIG.baseUrl}/${id}`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(updateData),
            });
            
            console.log('Update response status:', response.status);
            
            if (response.ok) {
                console.log('Profile updated successfully');
                return true;
            } else {
                const errorText = await response.text();
                console.error("Failed to update profile:", response.status, errorText);
                alert('Kunne ikke opdatere profilen: ' + response.status + '\n' + errorText);
                return false;
            }
        } catch (error) {
            console.error("Error:", error);
            alert('Fejl ved opdatering: ' + error.message);
            return false;
        }
    }
};
