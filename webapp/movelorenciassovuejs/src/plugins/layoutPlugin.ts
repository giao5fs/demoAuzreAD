import type { App } from "vue";

import DefaultLayout from "@/components/DefaultLayout.vue";
import EmptyLayout from "@/components/EmptyLayout.vue";
import { LayoutType } from "@/layoutType";

export const layoutPlugin = {
  install: (app: App) => {
    app.component(LayoutType.Default, DefaultLayout);
    app.component(LayoutType.Empty, EmptyLayout);
  },
};
