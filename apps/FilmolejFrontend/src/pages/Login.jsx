import { useState } from "react";
import { login } from "../api/auth";

function Login(){
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleLogin = async (e) => {
        e.preventDefault();

        try{
            const data = await login(email, password);

            localStorage.setItem("token", data.token);

            console.log("Logged in:", data);
            alert("Login success!");
        } catch (err) {
            setError("Wrong email or password");
        }
    };

    return (
        <div>
            <h2>Login</h2>

            <form onSubmit={handleLogin}>
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

                <button type="submit">Log In</button>
            </form>
            {error && <p style={{ color: "red" }}>{error}</p>}
        </div>
    );
}

export default Login;