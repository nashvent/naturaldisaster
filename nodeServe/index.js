var app=require('express')();
var server=require('http').Server(app);
var io=require('socket.io')(server);


server.listen(3000);
//server.listen(3000,"192.168.1.10");
app.get('/',function(req,res){
	res.send('Unity server');
});

var clients=[]; //Lista de clientes 
var objetos=[]; //Lista de objetos 
var meteoritolist=[];
var i = 0;
while (i < 2) {
    obj1={
    	id:i,
    	position:{x:60+(i*2)*2,y:2,z:79+i},
    	tipo:i%2,
    };
    objetos.push(obj1);
    i++;
} 

var posID=0;


io.on('connection',function (socket){
	var currentPlayer={};
	currentPlayer.name="G"+socket.id;
	currentPlayer.posID=clients.length;
	currentPlayer.socketID=socket.id;
	clients.push(currentPlayer);
	
	socket.on('player connect',function(){
		console.log('Jugador conectado');
		socket.emit('crearobj',{objetos});
		console.log("mi socket id ",socket.id);
		/*
		while(true){
			setTimeout(raingenerate, 2000, socket);
		}*/
		//socket.emit('pmovimiento');
	});



	socket.on('cposition',function(data){
		clients[currentPlayer.posID].position=data;
		nclients=[];
		for(var i=0;i<clients.length;i++){
			if(socket.id!=clients[i].socketID){
				nclients.push(clients[i]);	
			}
		}
		socket.emit('pmovimiento',{nclients,objetos});
		//console.log(data);
	});


	socket.on('disconnect',function(){
		console.log('JUgador desconecta');
		//clients.splice(currentPlayer.posID, 1);
	});

	socket.on('oposition',function(data){
		
		
		var id = data.id;
		//console.log(id);
		objetos[id].position = {x:data.x, y:data.y, z:data.z};
		console.log(objetos);
		var dis = distance(objetos[0].position,objetos[1].position);
		if(dis<3){
			var pos = objetos[0].position;
			console.log("distance corta",dis);
			io.emit("ocrear",{pos})
		}
		else{
			socket.broadcast.emit('omovimiento',{objetos});
		}
	});

});

function distance( v1, v2 )
{
    var dx = v1.x - v2.x;
    var dy = v1.y - v2.y;
    var dz = v1.z - v2.z;

    return Math.round(Math.sqrt( dx * dx + dy * dy + dz * dz ));
}


var os = require( 'os' );
var networkInterfaces = os.networkInterfaces( )['Conexión de red inalámbrica'][1].address;

console.log('-- server is running --');
console.log('-- ' +networkInterfaces+ ' --' );



/*GENERAR METEORITOS*/
const wt = require("worker-thread");
 
var tiempo=100;
var ct=0.1;
function worker(n) {
  return new Promise(r => {
    const second = tiempo;
    setTimeout(() => r(wf(n)), second*5 );
    
  });
}
 
const ch = wt.createChannel(worker, 1);
 

function randomInRange(min, max) {
  return Math.random() < 0.5 ? ((1-Math.random()) * (max-min) + min) : (Math.random() * (max-min) + min);
}

function wf(h){
	var met={};
	met.id=h;
	var rndx=randomInRange(0,250);
	var rndz=randomInRange(0,250);
	met.position={x:rndx,y:50,z:rndz};
	meteoritolist.push(met);
	metlist=[met];
	io.emit('rmet',{metlist});
	return 'hilo:'+h;
}


ch.on("done", (err, result) => {
  if (err) {
    console.error(err);
  }
 
  //console.log(result);
});
 
ch.on("stop", () => {
  console.log("channel is stop");
});
 


let it = 0;
while(it<20000) {
  ch.add(it++);
}



//		http://localhost:3000/