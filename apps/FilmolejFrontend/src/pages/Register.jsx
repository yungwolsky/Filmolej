import { use, useState } from "react";
import { register } from "../api/auth";

function Register(){
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleRegister = async (e) => {
        e.preventDefault();

        try{
            const data = await register(username, email, password);

            localStorage.setItem("token", data.token);

            console.log("User created:", data);
            alert("User created!");
        } catch (err) {
            setError("User with this email already exists");
        }
    };

    return (
        <div>
            <h2>Register</h2>

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
            </form>
            {error && <p style={{ color: "red" }}>{error}</p>}
        </div>
    )
}

export default Register;