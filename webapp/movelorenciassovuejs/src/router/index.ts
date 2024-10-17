import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import axios from "axios";
import { LayoutType } from "@/layoutType";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      component: () => import("../views/HomeView.vue"),
      meta: {
        layout: LayoutType.Default,
        requiresAuth: true,
      },
    },
    {
      path: "/about",
      name: "about",
      component: () => import("../views/AboutView.vue"),
      meta: {
        layout: LayoutType.Default,
        requiresAuth: true,
      },
    },
  ],
});

router.beforeEach(async (to, from, next) => {
  if (to.matched.some((x) => x.meta.requiresAuth)) {
    const token = localStorage.getItem("id_token");

    if (!token) {
      const urlParams = new URLSearchParams(window.location.search);
      const authorizationCode = urlParams.get("code");

      if (authorizationCode) {
        try {
          const token = await exchangeCodeForToken(authorizationCode);

          localStorage.setItem("id_token", token.id_token);
          localStorage.setItem("access_token", token.access_token);
          localStorage.setItem("refresh_token", token.refresh_token);

          window.history.replaceState({}, document.title, to.path);

          next();
        } catch (error) {
          console.error("Error exchanging code for tokens: ", error);
          next("/");
        }
      } else {
        window.location.href = buildLoginUrl();
      }
    } else {
      next();
    }
  } else {
    next();
  }
});

function buildLoginUrl() {
  const cognitoDomain = "movelorenciassodemo.auth.us-east-1.amazoncognito.com";
  const clientId = "74ttk0mit4f6ugvchd65vb4ic5";
  const redirectUri = "http://localhost:8080";
  const responseType = "code";
  const scopes = "email openid phone";
  const loginUrl = `https://${cognitoDomain}/login?response_type=${responseType}&client_id=${clientId}&redirect_uri=${redirectUri}&scope=${encodeURIComponent(
    scopes
  )}`;
  return loginUrl;
}

async function exchangeCodeForToken(code: string) {
  const cognitoDomain = "movelorenciassodemo.auth.us-east-1.amazoncognito.com";
  const clientId = "74ttk0mit4f6ugvchd65vb4ic5";
  const redirectUri = "http://localhost:8080";
  const tokenUrl = `https://${cognitoDomain}/oauth2/token`;

  try {
    const response = await axios.post(
      tokenUrl,
      new URLSearchParams({
        grant_type: "authorization_code",
        client_id: clientId,
        code: code,
        redirect_uri: redirectUri,
      }),
      {
        headers: {
          "Content-Type": "application/x-www-form-urlencoded",
        },
      }
    );

    return {
      id_token: response.data.id_token,
      access_token: response.data.access_token,
      refresh_token: response.data.refresh_token,
    };
  } catch (error) {
    throw new Error("Failed to exchange code for tokens");
  }
}

export default router;
