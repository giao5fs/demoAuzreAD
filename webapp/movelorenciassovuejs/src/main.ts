import "./assets/main.css";

import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import { layoutPlugin } from "./plugins/layoutPlugin";

const app = createApp(App);
app.use(layoutPlugin);
app.use(router);

app.mount("#app");
