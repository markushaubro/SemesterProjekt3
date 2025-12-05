const { createApp } = Vue;

createApp({
    data() {
        return {
            player: null
        };
    },
    computed: {
        accuracy() {
            if (!this.player || this.player.totalShots === 0) return 0;
            return Math.round((this.player.targetsTakenOut / this.player.totalShots) * 100);
        },
        avgValue() {
            if (!this.player || this.player.totalGames === 0) return 0;
            return this.player.totalBountyCollected / this.player.totalGames;
        }
    },
    methods: {
        formatCurrency(val) {
            return new Intl.NumberFormat("en-US", {
                style: "currency",
                currency: "USD",
                maximumFractionDigits: 0
            }).format(val);
        },
        async loadPlayer() {
            const response = await fetch("http://localhost:5098/api/scoreboard/1");
            this.player = await response.json();
        }
    },
    mounted() {
        this.loadPlayer();
    }
}).mount("#app");