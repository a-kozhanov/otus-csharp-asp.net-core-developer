import React, { Component } from 'react';
import { Route } from 'react-router';
import './App.css';
import { Layout } from '../Layout/Layout';
import { Home } from '../Home/Home';
import Dashboard from '../Dashboard/Dashboard';
import Preferences from '../Preferences/Preferences';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home}/>
                <Route path='/dashboard' component={Dashboard}/>
                <Route path='/preferences' component={Preferences}/>
            </Layout>
        );
    }
}