(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["createUser"],{"459d":function(e,s,t){},e213:function(e,s,t){"use strict";t("459d")},f6a5:function(e,s,t){"use strict";t.r(s);var a=function(){var e=this,s=e.$createElement,t=e._self._c||s;return t("v-card",{staticClass:"card-form",attrs:{elevation:"2"}},[t("v-form",{ref:"form",attrs:{"lazy-validation":""},on:{submit:function(s){return s.preventDefault(),e.onSubmit(s)}},model:{value:e.valid,callback:function(s){e.valid=s},expression:"valid"}},[t("h1",[e._v("Create User")]),t("div",{staticClass:"form-group"},[t("v-card",{staticClass:"mx-auto",attrs:{elevation:"0",outlined:""}},[t("v-container",[t("v-text-field",{staticClass:"form-control",attrs:{rules:[e.rules.required],label:"FirstName*","hide-details":"auto"},model:{value:e.user.first_name,callback:function(s){e.$set(e.user,"first_name",s)},expression:"user.first_name"}}),t("v-text-field",{staticClass:"form-control",attrs:{rules:[e.rules.required],label:"LastName*","hide-details":"auto"},model:{value:e.user.last_name,callback:function(s){e.$set(e.user,"last_name",s)},expression:"user.last_name"}}),t("v-text-field",{staticClass:"form-control",attrs:{rules:[e.rules.required],label:"Username*","hide-details":"auto"},model:{value:e.user.username,callback:function(s){e.$set(e.user,"username",s)},expression:"user.username"}}),t("v-text-field",{staticClass:"form-control",attrs:{rules:[e.rules.required,e.rules.email],label:"Email*","hide-details":"auto"},model:{value:e.user.email,callback:function(s){e.$set(e.user,"email",s)},expression:"user.email"}}),t("v-text-field",{staticClass:"form-control",attrs:{rules:[e.rules.required],label:"Password*",type:"password","hide-details":"auto"},model:{value:e.user.password,callback:function(s){e.$set(e.user,"password",s)},expression:"user.password"}}),t("v-text-field",{staticClass:"form-control",attrs:{rules:[e.rules.required,e.passwordConfirmationRule],label:"RepeatPassword*",type:"password","hide-details":"auto"},model:{value:e.repeatPassword,callback:function(s){e.repeatPassword=s},expression:"repeatPassword"}}),e.isAdmin?t("v-select",{attrs:{items:e.roles,rules:[e.rules.required],"item-text":"name","item-value":"id",label:"Select a Role*","return-value":"","hide-details":"auto"},model:{value:e.roleId,callback:function(s){e.roleId=s},expression:"roleId"}}):e._e()],1)],1),t("v-card-actions",[t("v-row",{attrs:{justify:"center"}},[e.submit?t("v-btn",{staticClass:"submit",attrs:{color:"primary",loading:"",disabled:!e.valid}}):t("v-btn",{staticClass:"submit",attrs:{type:"submit",elevation:"2",color:"primary",disabled:!e.valid}},[t("span",[e._v(" Create ")])]),t("v-btn",{attrs:{elevation:"2"},on:{click:function(s){return e.$router.go(-1)}}},[e._v("Back")])],1)],1)],1)])],1)},r=[],i=t("365c"),l=t("2f62"),o={data(){return{valid:!1,submit:!1,user:{username:"",email:"",first_name:"",last_name:"",password:"",roles:[]},roles:[],roleId:"00000000-0000-0000-0000-000000000000",repeatPassword:"",rules:{required:e=>!!e||"Required.",email:e=>{const s=/^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;return s.test(e)||"Invalid e-mail."}}}},methods:{...Object(l["b"])(["doLogin"]),onSubmit:function(){this.submit=!0,this.$refs.form.validate()?(""!=this.roleId&&(this.user.roles=[{id:this.roleId}]),this.isAdmin?Object(i["e"])(this.user).then(e=>{this.$router.push({name:"ListUser"})}).catch(e=>{console.log(e.response),this.submit=!1}):Object(i["v"])(this.user).then(e=>{Object(i["r"])(this.user).then(s=>{Object(i["b"])(s.data.token_type,s.data.access_token),Promise.all([this.$store.dispatch("ActionSetUser",e.data)]).finally(()=>{this.doLogin(s.data),this.$router.push({name:"ListRecipe"})})}).catch(e=>{console.log(e),this.submit=!1})}).catch(e=>{console.log(e.response),this.submit=!1})):this.submit=!1},getRoles(){Object(i["o"])().then(e=>{this.roles=e.data,this.roles.push({id:"00000000-0000-0000-0000-000000000000",name:"User"})}).catch(e=>{console.log(e.response)})}},created(){this.isAdmin&&this.getRoles()},computed:{passwordConfirmationRule(){return()=>this.user.password===this.repeatPassword||"Password must match"},...Object(l["c"])(["isAdmin"])}},n=o,u=(t("e213"),t("2877")),d=t("6544"),c=t.n(d),m=t("8336"),b=t("b0af"),f=t("99d9"),h=t("a523"),p=t("4bd4"),v=t("0fd9"),w=t("b974"),x=t("8654"),C=Object(u["a"])(n,a,r,!1,null,"2b65db14",null);s["default"]=C.exports;c()(C,{VBtn:m["a"],VCard:b["a"],VCardActions:f["a"],VContainer:h["a"],VForm:p["a"],VRow:v["a"],VSelect:w["a"],VTextField:x["a"]})}}]);
//# sourceMappingURL=createUser.b26ec5b8.js.map