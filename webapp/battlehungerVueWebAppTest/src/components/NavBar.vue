<template>
  <h3>Welcome to Summoner Rift!!!</h3>
  <nav class="nav-container">
    <a
      class="newbtn"
      href="https://login.microsoftonline.com/d9f9f390-21e0-4122-8bc7-741f92e7211d/saml2?SAMLRequest=fZLLbtswFET3%2FQqBe8oiI1QmYSkwGqQwkG7y6mMTsNSVQ0DkVXkpo%2BnXl5bjotlkSWJmDjHDzeVvPxYHiOQwtEyUFSsgWOxd2Lfs4f6ar9ll92FDxo9y0ts5PYdb%2BDUDpWJLBDFl3ycMNHuIdxAPzsLD7U3LnlOaSK9WHg8wYsyZzhBhDx5Lk1PKmTgYSlyUxps%2FmJn74BKWFv1qoa1cP0WgKYcDK64y0QWTlmeew0fcu1B6ZyMSDgnD6AIsCb0a1HChKi4FVLwWUvL1T9vwphaDktBIIfoThhW7q5Y91cp%2BNFIJPth1NthBcSVqwUHZuld9Xw9WZSnRDLtAyYTUMlnJmouKi%2Ba%2BqrW80FKVshE%2FWPF4LlQeC80VB9KnCls2x6DRkCMdjAfSyeq77ZcbnaV6ipjQ4si6U%2BN6AcbiGqM36X3v8cb1fFikGkJy6eUN%2B327Oa%2FJuqPsNIp%2BXUXTpP8N9vR991W9fBuaz5vV%2F6%2FsXo9vv0n3Fw%3D%3D&RelayState=H4sIAAAAAAAAAD1Q23KbMBT8Fz1bDjICJN5o7ThOHMdgYzvtdDxCiEuREBeZXDr996p96Nvu7J6dPfsLMBCC2wgFGw1E19fNmX5cimANZiCzyqo1A9ssLeOWBdiYxlG1wYV_Kyde5b43ZbjmnjXk1lAZ04V3d1JzJis9mpA4xLGasBrXubCwsPAQPW8tLEH4HQjFammJ7kRb5xZ0lW4F-DEDzT_nz25BTlUyretIklWqp5LqPcQk_rb9clzF95fy3PXNsSmi85v3qN348OIiVtBo__XUXx-QkDyb3tU9bolKy3SLg7RHlzj4fFfRete423iHH3qSvJ6fkUHLJT55q6N8lG1CnZemijdsiFkbCZ93pVT5rh-HQ5_uWbEum-Tj6QklcYKFO21sdWkbKz0JqQfR8pqNo_1Z6Tm7mWr-f-Q5U-xTt1yXbW30nGtlTxUIUbCgyKWOQ-0IICyYHMUMDH-nIx5BnAdQII4hXrgBzLjHoYd9kTESZI7v2wxjrVdMuc9sECw4cSDmBYUUYQQF5TineY4LTsHvP7w92j34AQAA.H4sIAAAAAAAAAHO8k19xt0hO9b5czbrYbj4N-VebN3VmLeDjshG0tQy9vRUAs35qySAAAAA.4"
    >
      Login SSO
    </a>
    <router-link to="/"><button>Home</button></router-link>
    <router-link to="/profile"
      ><button>Profile protected by Guard</button></router-link
    >
    <router-link to="/profilenoguard"
      ><button>Profile protected no Guard</button></router-link
    >
    <SignOutButton v-if="isAuthenticated" />
    <SignInButton v-else />
  </nav>
</template>

<script setup lang="ts">
import { useIsAuthenticated } from "../composition-api/useIsAuthenticated";
import SignInButton from "./SignInButton.vue";
import SignOutButton from "./SignOutButton.vue";

const isAuthenticated = useIsAuthenticated();
</script>

<style>
a {
  text-decoration: none;
  color: white;
}

.newbtn {
  padding: 3px;
  box-sizing: border-box;
  align-content: center;
  font-size: 13.3px;
  color: rgb(0, 0, 0);
  background-color: rgb(239, 239, 239);
  font-family: Arial, Helvetica, sans-serif;
  border: 1px solid black;
  border-radius: 3px;
  height: 21px;
  font-weight: 400;
  text-align: center;
  display: inline-block;
  border-color: rgb(0, 0, 0);
  word-spacing: 0px;
}

#authButton {
  margin-left: auto;
}

.container {
  display: flex;
  justify-content: space-between;
}

h3 {
  position: absolute;
  top: -15px;
  line-height: 34px;
  color: blue;
}

.nav-container {
  display: flex;
  justify-content: right;
  gap: 10px;
  flex-direction: row;
  flex-wrap: nowrap;
  margin-bottom: 10px;
}
</style>
