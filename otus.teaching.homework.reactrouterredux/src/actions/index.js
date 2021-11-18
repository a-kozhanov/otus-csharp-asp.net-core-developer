export const LOGIN = "LOGIN";
export const REGISTER = "REGISTER";

export const login = () => ({
    type: LOGIN
})

export const register = (email, password) => ({
    type: REGISTER,
    data: {email, password}
})