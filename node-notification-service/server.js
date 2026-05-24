const express = require('express');

const app = express();

app.use(express.json());

app.post('/notify', async (req, res) => {
    const payload = req.body;

    console.log('=================================');
    console.log('NOTIFICATION RECEIVED');
    console.log(payload);
    console.log('=================================');

    return res.status(200).json({
        success: true,
        message: 'Notification processed'
    });
});

app.get('/health', (req, res) => {
    return res.status(200).json({
        status: 'healthy'
    });
});

app.listen(4000, '0.0.0.0', () => {
    console.log('Notification service running on port 4000');
});
