// FBI Most Wanted API Service
const FBIService = {
    apiUrl: "https://fbidatabaseapi-ewcefwcaeyarfhgz.italynorth-01.azurewebsites.net/api/fbi",

    async getMostWanted(page = 1, pageSize = 10) {
        try {
            const response = await fetch(`${this.apiUrl}?page=${page}&pageSize=${pageSize}`);
            if (response.ok) {
                const data = await response.json();
                console.log('FBI Most Wanted data:', data);
                return data;
            } else {
                console.error("Failed to fetch FBI most wanted");
                return null;
            }
        } catch (error) {
            console.error("Error fetching FBI data:", error);
            return null;
        }
    }
};
