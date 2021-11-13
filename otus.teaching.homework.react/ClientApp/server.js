const express = require('express');
const cors = require('cors')
const app = express();

app.use(cors());
app.use(express.json());

app.post('/login', (req, res) => {
    console.log(req.body);
    res.send({
        token: '123456'
    });
});

app.listen(8080, () => console.log('API is running on http://localhost:8080/login'));