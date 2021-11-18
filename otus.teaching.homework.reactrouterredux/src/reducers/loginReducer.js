import { LOGIN } from "../actions";

const initialState = {isLoggedIn: false}

const loginReducer = (state = initialState, action) => {
  switch (action.type) {
    case LOGIN:
      return {
        isLoggedIn: true
      }
    default:
      return state;
  }
}

export default loginReducer;