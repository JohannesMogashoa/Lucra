import axios from "axios";
import https from "https"
import * as fs from "fs";

export default axios.create({
    baseURL: "http://localhost:5054",
    headers: {
        "Content-type": "application/json"
    },
    httpsAgent: new https.Agent({
        rejectUnauthorized: false
    })
});