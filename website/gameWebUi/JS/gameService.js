// Game API Service
const GameService = {
    async startGameSession(playerId) {
        try {
            console.log('Attempting to start game for player:', playerId);
            console.log('Request URL:', `${API_CONFIG.gameUrl}/Start`);
            console.log('Request body:', { ProfileId: playerId });
            
            const response = await fetch(`${API_CONFIG.gameUrl}/Start`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ ProfileId: playerId }),
            });
            
            console.log('Response status:', response.status);
            const responseText = await response.text();
            console.log('Response body:', responseText);
            
            if (response.ok) {
                console.log('Game started for player:', playerId);
                return { success: true };
            } else {
                console.error("Failed to start game. Status:", response.status);
                alert('Kunne ikke starte spillet. Status: ' + response.status + '\n' + responseText);
                return { success: false };
            }
        } catch (error) {
            console.error("Error:", error);
            alert('Fejl ved start af spil: ' + error.message);
            return { success: false };
        }
    },

    async endGameSession() {
        try {
            const response = await fetch(`${API_CONFIG.gameUrl}/End`, {
                method: "POST",
            });
            if (response.ok) {
                console.log('Game ended');
                return true;
            } else {
                console.error("Failed to end game");
                return false;
            }
        } catch (error) {
            console.error("Error:", error);
            return false;
        }
    },

    async getCurrentPlayer() {
        try {
            const response = await fetch(`${API_CONFIG.gameUrl}/Current`);
            if (response.ok) {
                const data = await response.json();
                console.log('Current player data:', data);
                return data;
            } else {
                console.error("Failed to get current player");
                return null;
            }
        } catch (error) {
            console.error("Error:", error);
            return null;
        }
    },

    async addScore(points) {
        try {
            const response = await fetch(`${API_CONFIG.gameUrl}/AddScore`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ points: points }),
            });
            if (response.ok) {
                console.log('Score added:', points);
                return true;
            } else {
                console.error("Failed to add score");
                return false;
            }
        } catch (error) {
            console.error("Error:", error);
            return false;
        }
    }
};
