module.exports = {
  publicPath: process.env.NODE_ENV === 'production' ? 'dist/' : '/',
  transpileDependencies: [
    'vuetify'
  ],
  css: {
    loaderOptions: {
      scss: {
        additionalData: `@import "@/assets/scss/_variables.scss";
        `,
      },
    },
  },
}