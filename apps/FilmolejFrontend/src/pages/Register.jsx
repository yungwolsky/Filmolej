import { useState } from "react";
import { register } from "../api/auth";
import { Link } from "react-router-dom";

function Register(){
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleRegister = async (e) => {
        e.preventDefault();

        try {
            const response = await register(username, email, password);

            const data = response.data; 

            localStorage.setItem("token", data.token);

            setError("");
            alert("User created!");
        } catch (err) {
            const message =
                err.response?.data?.message ||
                err.response?.data ||
                "Registration failed";

            setError(message);
        }
    }

    return (
        <div>
            <h2>Register</h2>
            {error && <p style={{ color: "red" }}>{error}</p>}

            <form onSubmit={handleRegister}>
                <input 
                    type="username" 
                    placeholder="Username"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                />

                <br />

                <input 
                    type="email" 
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />

                <br />

                <input 
                    type="password" 
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />

                <br />

                <button type="submit">Register</button>              
                <Link to="/login">Already have an account?</Link>
            </form>
        </div>
    )
}

export default Register;