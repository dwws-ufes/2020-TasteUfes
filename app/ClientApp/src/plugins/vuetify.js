import Vue from 'vue';
import Vuetify from 'vuetify/lib/framework';

import ptBR from '@/lang/pt-BR';
import enUS from '@/lang/en-US';

Vue.use(Vuetify);

export default new Vuetify({
    theme: {
        themes: {
            light: {
                primary: '#4caf50',
            },
        },
    },

    lang: {
        locales: { ptBR, enUS },
        current: navigator.language === 'pt-BR' ? 'ptBR' : 'enUS',
    },
});
