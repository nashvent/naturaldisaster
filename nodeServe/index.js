var app=require('express')();
var server=require('http').Server(app);
var io=require('socket.io')(server);

server.listen(3000);
//server.listen(3000,"192.168.1.10");
app.get('/',function(req,res){
	res.send('Chasho');
});

io.on('connection',function (socket){
	socket.on('player connect',function(){
		console.log('JUgador connectador')
	});

	socket.on('disconnect',function(){
		console.log('JUgador desconecta')
	});
});


console.log('servidor de unity corriendo');
//		http://localhost:3000/