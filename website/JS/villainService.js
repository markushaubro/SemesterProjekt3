// Villain API Service
const VillainService = {
    // Save a villain to the database
    // Schema: Id (auto), Title, Latitude, Longitude, MaxReward, IsActive, CaughtAt, CaughtByUserId
    async saveVillain(villainData) {
        try {
            console.log('Saving villain to database:', villainData);
            const response = await fetch(API_CONFIG.villainUrl, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    Title: villainData.title,
                    Latitude: villainData.latitude,
                    Longitude: villainData.longitude,
                    MaxReward: villainData.maxReward,
                    IsActive: true,
                    CaughtAt: null,
                    CaughtByUserId: null
                }),
            });
            
            if (response.ok) {
                const result = await response.json();
                console.log('Villain saved successfully:', result);
                return result;
            } else {
                console.error("Failed to save villain. Status:", response.status);
                return null;
            }
        } catch (error) {
            console.error("Error saving villain:", error);
            return null;
        }
    },

    // Delete all villains from the database
    async deleteAllVillains() {
        try {
            console.log('Deleting all villains from database...');
            const response = await fetch(`${API_CONFIG.villainUrl}/all`, {
                method: "DELETE",
            });
            
            if (response.ok) {
                console.log('All villains deleted successfully');
                return true;
            } else {
                console.error("Failed to delete villains. Status:", response.status);
                return false;
            }
        } catch (error) {
            console.error("Error deleting villains:", error);
            return false;
        }
    }
};
