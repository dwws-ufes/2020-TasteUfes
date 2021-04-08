import { store } from "@/auth";

export default async (to, from, next) => {
    document.title = `${to.name} - TasteUfes`;
    if (store.state.token != null) {
        next();
    } else {
        if (to.name !== "Home") {
            console.log("Redirec")
            next({ name: "Home" });
        } else {
            next();
        }
    }
}