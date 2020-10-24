const path = require('path');
const express = require('express');
const app = express();

const WebSocket = require('ws');
const WebSocket1 = require('ws');

const wss = new WebSocket.Server({ port: 8080 });
const hss = new WebSocket1.Server({port: 3000});

wss.on('connection', function connection(ws,req) {
	ws.on('message', data => { 
		if (ws.readyState === ws.OPEN) {
			hss.on('connection', function connection(ws,req) {
				ws.send(data);           
			}); 
		}
		console.log('received from unity: %s',JSON.stringify(data));
	});
  //ws.send('Hello! Message From Server!!'); 
});