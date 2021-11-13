import React from 'react';
import useToken from "../App/useToken";
import Login from "../Login/Login";

export default function Dashboard() {
    const {token, setToken} = useToken();
    if (!token) {
        return <Login setToken={setToken}/>
    }

    return (
        <h2>Dashboard</h2>
    );
}