<template>
  <header>
    <div class="wrapper">
      <nav class="nav-container">
        <RouterLink class="link" to="/">Home</RouterLink>
        <RouterLink class="link" to="/about">About</RouterLink>
      </nav>
    </div>
  </header>
</template>
<style scoped>

.nav-container {
  display: flex;
  justify-content: left;
  gap: 10px;
}

.link {
  text-decoration: none;
  color: black;
}

.link:hover {
  color: green;
  opacity: 70%;
}
</style>
