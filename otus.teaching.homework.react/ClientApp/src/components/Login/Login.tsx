import React, {Dispatch, FormEvent, SetStateAction, useState} from 'react';
import PropTypes from 'prop-types';
import axios from 'axios';
import './Login.css';

interface UserCredentials {
    username?: string | undefined,
    password?: string | undefined
}

async function loginUser(credentials: UserCredentials) {
    return axios.post('/login', JSON.stringify(credentials), 
        {
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(resp => {
            return {
                token: resp.data,
                error: null
            }
        })
        .catch(err => {
            return {
                token: undefined,
                error: err.message
            }
        });
}

export default function Login({ setToken }: { setToken: Dispatch<SetStateAction<any>> }) {
    const [username, setUserName] = useState<string>();
    const [password, setPassword] = useState<string>();
    const [error, setError] = useState<string>();
    
    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        const {token, error} = await loginUser({
            username,
            password
        });
        if (token)
            setToken(token);
        if (error)
            setError(error);
    }
    
    return(
        <div className="login-wrapper">
            <h1>Please Log In</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <p>Username</p>
                    <input type="text" onChange={e => setUserName(e.target.value)}/>
                </div>
                <div>
                    <p>Password</p>
                    <input type="password" onChange={e => setPassword(e.target.value)}/>
                </div>
                <div>
                    <br/>
                    <button type="submit">Submit</button>
                </div>
                <div>
                    <br/>
                    <label>{error}</label>
                </div>
            </form>
        </div>
    )
}

Login.propTypes = {
    setToken: PropTypes.func.isRequired
}