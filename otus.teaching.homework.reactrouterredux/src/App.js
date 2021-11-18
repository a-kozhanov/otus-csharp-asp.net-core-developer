import React from "react";
import {BrowserRouter as Router, Switch, Route, Redirect,} from 'react-router-dom';
import {Navbar, Nav, NavDropdown, Form, FormControl, Button} from 'react-bootstrap'
import {LinkContainer} from "react-router-bootstrap";
import {connect} from "react-redux";
import Register from "./components/Register";
import LogIn from "./components/LogIn";
import NotFound from "./components/NotFound";
import HomePage from "./components/HomePage";
import {register as registerAction, login as loginAction} from "./actions"
import {Container} from "reactstrap";

function App(props) {

    const login = (data) => {
        if (props.email === data.email && props.password === data.password) {
            props.dispatch(loginAction())
            return true;
        }
        return false;
    }

    const register = (data) => {
        props.dispatch(registerAction(data.email, data.password))
        return true;
    }

    return (
        <Router>
            <Container>
                <Navbar color="light" variant="light" expand="lg">
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Navbar.Brand href="/home">React Router&Redux</Navbar.Brand>
                        <Nav className="mr-auto">
                            <LinkContainer to="/home">
                                <NavDropdown.Item>Домашняя страница</NavDropdown.Item>
                            </LinkContainer>
                            <LinkContainer to="/register">
                                <NavDropdown.Item>Регистрация</NavDropdown.Item>
                            </LinkContainer>
                            <LinkContainer to="/login">
                                <NavDropdown.Item>Вход</NavDropdown.Item>
                            </LinkContainer>
                        </Nav>
                    </Navbar.Collapse>
                    {props.isLoggedIn && <Navbar.Text>Здравствуйте, {props.email}!</Navbar.Text>}
                </Navbar>

                <Switch>
                    <Route exact path='/home'>
                        <HomePage text={JSON.stringify(props, null, '\t')}/>
                    </Route>

                    <Route path='/register'>
                        <Register registerCallback={register}/>
                    </Route>

                    <Route path='/login'>
                        <LogIn loginCallback={login}/>
                    </Route>

                    <Route path='*'>
                        <NotFound/>
                    </Route>
                </Switch>
            </Container>
        </Router>
    );
}

function mapStateToProps(state) {
    return {
        ...state.registerReducer,
        ...state.loginReducer
    }
}

export default connect(mapStateToProps)(App);
