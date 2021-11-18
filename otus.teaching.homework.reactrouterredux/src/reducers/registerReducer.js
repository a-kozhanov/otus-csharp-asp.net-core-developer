import {REGISTER} from "../actions";

const initialState = {email: "", password: ""}

const registerReducer = (state = initialState, action) => {
  switch (action.type) {
    case REGISTER:
      return {...state, email: action.data.email, password: action.data.password}
    default:
      return state;
  }
}

export default registerReducer;