var app=require('express')();
var server=require('http').Server(app);
var io=require('socket.io')(server);

server.listen(3000);
//server.listen(3000,"192.168.1.10");
app.get('/',function(req,res){
	res.send('Unity server');
});

var clients=[];
var players={};
var posID=0;

var pruebaPlayer={};
pruebaPlayer.name="nasho";
pruebaPlayer.posID=0;
pruebaPlayer.position={x:4,y:2.8,z:4};
clients.push(pruebaPlayer);
io.on('connection',function (socket){
	var currentPlayer={};
	currentPlayer.name="G"+socket.id;
	currentPlayer.posID=clients.length;
	clients.push(currentPlayer);
	
	socket.on('player connect',function(){
		console.log('JUgador conectado');
		for(var i=0;i<clients.length;i++){
			console.log("J: ",JSON.stringify(clients[i]));
		}
		//socket.emit('pmovimiento');
	});

	socket.on('cposition',function(data){
		//currentPlayer.data=data;
		clients[currentPlayer.posID].position=data;
		nclients=[];
		for(var i=0;i<clients.length;i++){
			nclients.push(clients[i]);
		}
		socket.emit('pmovimiento',{nclients});
		console.log(data);
	});


	socket.on('disconnect',function(){
		console.log('JUgador desconecta');

	});


	
});

console.log('-- server is running --');
//		http://localhost:3000/