const { createApp } = Vue;

createApp({
    data() {
        return {
            player: null
        };
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