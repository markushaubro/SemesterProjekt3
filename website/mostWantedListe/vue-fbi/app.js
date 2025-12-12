const app = Vue.createApp({
  data() {
    return {
      wanted: [],
      loading: true,
      error: null
    };
  },

  async mounted() {
    try {
      // Når backend er lavet skal vi udskifte URL'en
      const response = await fetch("http://localhost:5000/api/fbi/top10");

      if (!response.ok) throw new Error("Kunne ikke hente data");

      this.wanted = await response.json();
    } catch (err) {
      this.error = err.message;
    } finally {
      this.loading = false;
    }
  }
});

app.mount("#app");
