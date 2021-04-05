<template>
  <div class="home">
    <Recipes :isTable='true' />
  </div>
</template>

<script>
// @ is an alias to /src
import Recipes from "@/views/Recipes/Index.vue";

export default {
  name: 'Home',
  components: {
    Recipes,
  }
}
</script>
