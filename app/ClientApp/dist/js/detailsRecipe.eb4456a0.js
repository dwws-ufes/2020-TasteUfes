(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["detailsRecipe"],{"0546":function(t,e,s){"use strict";s("95eb")},"1f09":function(t,e,s){},2957:function(t,e,s){"use strict";var a=function(){var t=this,e=t.$createElement,s=t._self._c||e;return s("v-container",[s("v-card-title",[s("v-row",[s("v-col",{staticClass:"d-flex justify-center",attrs:{cols:"12"}},[s("v-avatar",{attrs:{color:"primary",size:"64"}},[s("span",{staticClass:"white--text headline"},[t._v(" "+t._s(new String(t.user.first_name).charAt(0).toUpperCase())+t._s(new String(t.user.last_name).charAt(0).toUpperCase())+" ")])])],1),s("v-col",{staticClass:"d-flex justify-center",attrs:{cols:"12"}},[s("span",[t._v(t._s(t.user.first_name)+" "+t._s(t.user.last_name))])]),s("v-card-text",{staticClass:"d-flex justify-center pt-0"},[t.user.roles?s("v-row",[t.user.roles.length>0?s("v-col",{staticClass:"d-flex justify-center pb-0",attrs:{cols:"12"}},[s("span",[s("b",[t._v(t._s(t.user.roles[0].name))])])]):s("v-col",{staticClass:"d-flex justify-center pb-0",attrs:{cols:"12"}},[s("span",[s("b",[t._v("User")])])]),s("v-col",{staticClass:"d-flex justify-center pb-0",attrs:{cols:"12"}},[s("span",[t._v(t._s(t.user.email))])]),null!=t.username?s("v-col",{staticClass:"d-flex justify-center",attrs:{cols:"12"}},[s("span",[t._v("Username: "+t._s(t.user.username))])]):t._e()],1):t._e()],1)],1)],1),s("v-card-actions",[t.user.id==t.getUserId&&null!=t.username?s("v-row",[s("v-col",{staticClass:"d-flex justify-flex-end"},[s("v-btn",{staticClass:"primary",attrs:{to:{name:"EditUser",params:{id:t.user.id}}}},[s("v-icon",{staticClass:"mr-2",attrs:{small:""}},[t._v("mdi-pencil")]),t._v(" Edit")],1)],1)],1):t._e()],1)],1)},i=[],r=s("2f62"),n={props:{user:{roles:[]},username:String},computed:{...Object(r["c"])(["getUserId"])}},l=n,o=s("2877"),c=s("6544"),d=s.n(c),v=s("8212"),p=s("8336"),u=s("99d9"),h=s("62ad"),m=s("a523"),_=s("132d"),g=s("0fd9"),f=Object(o["a"])(l,a,i,!1,null,null,null);e["a"]=f.exports;d()(f,{VAvatar:v["a"],VBtn:p["a"],VCardActions:u["a"],VCardText:u["b"],VCardTitle:u["c"],VCol:h["a"],VContainer:m["a"],VIcon:_["a"],VRow:g["a"]})},3129:function(t,e,s){"use strict";s("1f09");var a=s("c995"),i=s("24b2"),r=s("7560"),n=s("58df"),l=s("80d2");e["a"]=Object(n["a"])(a["a"],i["a"],r["a"]).extend({name:"VSkeletonLoader",props:{boilerplate:Boolean,loading:Boolean,tile:Boolean,transition:String,type:String,types:{type:Object,default:()=>({})}},computed:{attrs(){return this.isLoading?this.boilerplate?{}:{"aria-busy":!0,"aria-live":"polite",role:"alert",...this.$attrs}:this.$attrs},classes(){return{"v-skeleton-loader--boilerplate":this.boilerplate,"v-skeleton-loader--is-loading":this.isLoading,"v-skeleton-loader--tile":this.tile,...this.themeClasses,...this.elevationClasses}},isLoading(){return!("default"in this.$scopedSlots)||this.loading},rootTypes(){return{actions:"button@2",article:"heading, paragraph",avatar:"avatar",button:"button",card:"image, card-heading","card-avatar":"image, list-item-avatar","card-heading":"heading",chip:"chip","date-picker":"list-item, card-heading, divider, date-picker-options, date-picker-days, actions","date-picker-options":"text, avatar@2","date-picker-days":"avatar@28",heading:"heading",image:"image","list-item":"text","list-item-avatar":"avatar, text","list-item-two-line":"sentences","list-item-avatar-two-line":"avatar, sentences","list-item-three-line":"paragraph","list-item-avatar-three-line":"avatar, paragraph",paragraph:"text@3",sentences:"text@2",table:"table-heading, table-thead, table-tbody, table-tfoot","table-heading":"heading, text","table-thead":"heading@6","table-tbody":"table-row-divider@6","table-row-divider":"table-row, divider","table-row":"table-cell@6","table-cell":"text","table-tfoot":"text@2, avatar@2",text:"text",...this.types}}},methods:{genBone(t,e){return this.$createElement("div",{staticClass:`v-skeleton-loader__${t} v-skeleton-loader__bone`},e)},genBones(t){const[e,s]=t.split("@"),a=()=>this.genStructure(e);return Array.from({length:s}).map(a)},genStructure(t){let e=[];t=t||this.type||"";const s=this.rootTypes[t]||"";if(t===s);else{if(t.indexOf(",")>-1)return this.mapBones(t);if(t.indexOf("@")>-1)return this.genBones(t);s.indexOf(",")>-1?e=this.mapBones(s):s.indexOf("@")>-1?e=this.genBones(s):s&&e.push(this.genStructure(s))}return[this.genBone(t,e)]},genSkeleton(){const t=[];return this.isLoading?t.push(this.genStructure()):t.push(Object(l["r"])(this)),this.transition?this.$createElement("transition",{props:{name:this.transition},on:{afterEnter:this.resetStyles,beforeEnter:this.onBeforeEnter,beforeLeave:this.onBeforeLeave,leaveCancelled:this.resetStyles}},t):t},mapBones(t){return t.replace(/\s/g,"").split(",").map(this.genStructure)},onBeforeEnter(t){this.resetStyles(t),this.isLoading&&(t._initialStyle={display:t.style.display,transition:t.style.transition},t.style.setProperty("transition","none","important"))},onBeforeLeave(t){t.style.setProperty("display","none","important")},resetStyles(t){t._initialStyle&&(t.style.display=t._initialStyle.display||"",t.style.transition=t._initialStyle.transition,delete t._initialStyle)}},render(t){return t("div",{staticClass:"v-skeleton-loader",attrs:this.attrs,on:this.$listeners,class:this.classes,style:this.isLoading?this.measurableStyles:void 0},[this.genSkeleton()])}})},"5f5b":function(t,e,s){"use strict";var a=function(){var t=this,e=t.$createElement,s=t._self._c||e;return s("div",[t.load?s("v-sheet",{staticClass:"pa-3",attrs:{color:"grey lighten-4"}},[s("v-skeleton-loader",{staticClass:"mx-auto",attrs:{type:"article, list-item-three-line, list-item-three-line"}})],1):s("div",[s("v-card-title",{staticClass:"primary"},[t._v("Nutrition Facts")]),s("v-row",[s("v-col",[s("v-card-text",[s("v-divider",{staticClass:"mb-4"}),s("span",{staticClass:"py-1"},[t._v(t._s(t.servings)+" serving"+t._s(1!=t.servings?"s":"")+" per container")]),s("h3",{staticClass:"py-1"},[t._v(" Serving size "),s("span",{staticClass:"float-right"},[t._v(" "+t._s(t.data.serving_size)+t._s(t.getMeasureName(t.data.serving_size_unit))+" ")])]),s("v-divider",{staticClass:"my-4"}),s("v-row",[s("v-col",[s("h2",{staticClass:"py-1"},[t._v("Amount per serving")]),s("h3",{staticClass:"py-1"},[t._v(" Calories "),s("span",{staticClass:"float-right"},[t._v(t._s(parseInt(t.data.serving_energy)))])]),s("h3",{staticClass:"py-1"},[t._v(" Daily Value "),s("span",{staticClass:"float-right"},[t._v(t._s(t.getDailyValue(t.data.daily_value))+"%")])])])],1),s("v-divider",{staticClass:"my-4"}),s("v-row",[s("v-col",{staticClass:"pb-0"},[s("span",{staticClass:"float-right"},[s("h5",[t._v("% Daily Value*")])])]),s("v-col",{attrs:{cols:"12 pb-5"}},t._l(t.data.nutrition_facts_nutrients,(function(e){return s("div",{key:e.id},[s("h5",[s("v-divider"),t._v(" "+t._s(e.nutrient.name)+" "+t._s(parseInt(e.amount_per_serving))+t._s(t.getMeasureName(e.amount_per_serving_unit))+" "),s("span",{staticClass:"float-right"},[t._v(" "+t._s(t.getDailyValue(e.daily_value))+"% ")])],1)])})),0)],1)],1)],1)],1)],1)],1)},i=[],r={data(){return{load:!0}},props:{servings:Number,data:{nutrition_facts_nutrients:[{nutrient:Object}]}},mounted(){this.load=!1},methods:{getMeasureName(t){if(t>0)return this.$store.state.ingredients_measures.find(e=>e.id==t).name},getDailyValue(t){return(100*t).toFixed(2)}}},n=r,l=(s("d70f"),s("2877")),o=s("6544"),c=s.n(o),d=s("99d9"),v=s("62ad"),p=s("ce7e"),u=s("0fd9"),h=s("8dd9"),m=s("3129"),_=Object(l["a"])(n,a,i,!1,null,"559c17a8",null);e["a"]=_.exports;c()(_,{VCardText:d["b"],VCardTitle:d["c"],VCol:v["a"],VDivider:p["a"],VRow:u["a"],VSheet:h["a"],VSkeletonLoader:m["a"]})},"8c20":function(t,e,s){"use strict";s.r(e);var a=function(){var t=this,e=t.$createElement,s=t._self._c||e;return s("v-container",{staticClass:"details"},[s("v-row",{attrs:{justify:"center"}},[s("v-col",{staticClass:"py-0",attrs:{cols:"12",sm:"12","d-flex":"","justify-center":""}},[s("div",{staticClass:"d-flex"},[s("span",{staticClass:"back-btn",on:{click:function(e){return t.$router.go(-1)}}},[s("v-icon",[t._v("mdi-chevron-left")]),t._v(" Back ")],1)])]),s("v-col",{attrs:{cols:"12",sm:"8","d-flex":"","justify-center":""}},[t.load?s("v-sheet",{staticClass:"pa-3",attrs:{color:"grey lighten-4"}},[s("v-skeleton-loader",{staticClass:"mx-auto",attrs:{type:"article, list-item-three-line"}})],1):s("v-card",[s("v-card-title",{staticClass:"primary"},[s("h1",[t._v(t._s(t.recipe.name))])]),s("v-divider",{staticClass:"mx-4"}),s("v-list-item",[s("v-list-item-content",[s("div",{staticClass:"my-2"},[s("v-row",[s("v-col",{attrs:{justify:"space-around"}},[s("v-text-field",{attrs:{label:"Servings",type:"number",rules:this.limitRule},on:{change:function(e){return t.recalculate()}},model:{value:t.serv,callback:function(e){t.serv=t._n(e)},expression:"serv"}})],1)],1)],1),s("div",{staticClass:"my-2"},[s("b",[t._v("Preparation Time:")]),t._v(" "+t._s(this.recipe.preparation.preparation_time)+" ")])])],1),this.recipe.ingredients.length>0?s("v-list-item",[s("v-list-item-content",[s("h3",[t._v("Ingredients")]),s("v-divider",{staticClass:"px-1 pb-3"}),t._l(this.recipe.ingredients,(function(e){return s("v-list-item-content",{key:e.id},[s("span",[s("router-link",{staticClass:"text-decoration-none",attrs:{target:"_blank",to:{name:"DetailsFood",params:{id:e.food.id}}}},[s("b",[t._v(t._s(e.food.name)+":")])]),t._v(" "+t._s(t.formatNumber(e.quantity))+t._s(t.getMeasureName(e.quantity_unit))+" ")],1)])}))],2)],1):t._e(),this.recipe.preparation.steps.length>0?s("v-list-item",[s("v-list-item-content",[s("h3",[t._v("Steps")]),s("v-divider",{staticClass:"px-1 pb-3"}),t._l(this.recipe.preparation.steps,(function(e){return s("v-list-item-content",{key:e.step},[s("ul",[s("li",[s("span",[s("b",[t._v("Step "+t._s(e.step)+":")]),t._v(" "+t._s(e.description)+" ")])])])])}))],2)],1):t._e()],1)],1),s("v-col",{attrs:{cols:"12",sm:"4"}},[s("v-card",{staticClass:"mb-5"},[s("UserCard",{attrs:{user:this.recipe.user,username:null}})],1),s("v-card",[s("NutritionFactsTable",{attrs:{data:this.recipe.nutrition_facts,servings:this.recipe.servings}})],1)],1)],1)],1)},i=[],r=s("365c"),n=s("5f5b"),l=s("2957"),o={name:"DetailsRecipe",data(){return{load:!0,recipeId:this.$route.params.id,serv:null,limitRule:[t=>t<1e4||"Value too big",t=>t>0||"Value must not be negative or 0"],recipe:{name:"",servings:null,preparation:{steps:[]},ingredients:[],nutrition_facts:{},user:{roles:[]}}}},created:function(){this.getData()},methods:{getData:function(){Object(r["m"])(this.recipeId).then(t=>{this.recipe=t.data,this.recipe.preparation.steps.sort((t,e)=>t.step<e.step?-1:1),this.serv=this.recipe.servings}).catch(t=>{console.log(t.response)}).finally(()=>{this.load=!1})},getMeasureName(t){return this.$store.state.ingredients_measures.find(e=>e.id==t).name},getDailyValue(t){return(100*t).toFixed(2)},recalculate(){this.serv>0&&this.serv<1e4&&Object(r["s"])(this.recipe.id,this.serv).then(t=>{this.recipe=t.data,this.recipe.preparation.steps.sort((t,e)=>t.step<e.step?-1:1),this.recipe.ingredients.map(t=>{let e=t.quantity;t.quantity=parseFloat(e.toFixed(2))}),console.log(this.recipe)}).catch(t=>{console.log(t.response)})},formatNumber(t){return parseFloat(t.toFixed(2))}},components:{NutritionFactsTable:n["a"],UserCard:l["a"]}},c=o,d=(s("0546"),s("2877")),v=s("6544"),p=s.n(v),u=s("b0af"),h=s("99d9"),m=s("62ad"),_=s("a523"),g=s("ce7e"),f=s("132d"),y=s("da13"),b=s("5d23"),C=s("0fd9"),x=s("8dd9"),V=s("3129"),S=s("8654"),k=Object(d["a"])(c,a,i,!1,null,"40d47996",null);e["default"]=k.exports;p()(k,{VCard:u["a"],VCardTitle:h["c"],VCol:m["a"],VContainer:_["a"],VDivider:g["a"],VIcon:f["a"],VListItem:y["a"],VListItemContent:b["a"],VRow:C["a"],VSheet:x["a"],VSkeletonLoader:V["a"],VTextField:S["a"]})},"8ce9":function(t,e,s){},"95eb":function(t,e,s){},ce7e:function(t,e,s){"use strict";s("8ce9");var a=s("7560");e["a"]=a["a"].extend({name:"v-divider",props:{inset:Boolean,vertical:Boolean},render(t){let e;return this.$attrs.role&&"separator"!==this.$attrs.role||(e=this.vertical?"vertical":"horizontal"),t("hr",{class:{"v-divider":!0,"v-divider--inset":this.inset,"v-divider--vertical":this.vertical,...this.themeClasses},attrs:{role:"separator","aria-orientation":e,...this.$attrs},on:this.$listeners})}})},d054:function(t,e,s){},d70f:function(t,e,s){"use strict";s("d054")}}]);
//# sourceMappingURL=detailsRecipe.eb4456a0.js.map