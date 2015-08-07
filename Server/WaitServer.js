var io = require("socket.io").listen(8080);

io.configure(function(){  
    io.set('log level', 2);
});

 var jarray ={  //everything goes here     
 };
 
var socketRoom = {};

function A(){
    this.thrashNum = [5,2,4,1,3,0];
    this.thrashIdx =5;
    this.userNames = {};//id
    this.selectCharacter={};
    this.ready={};
    this.team={};
    this.userNum={};
}

io.sockets.on('connection', function (socket) {
    
//join room
    socket.on("joinRoomREQ",function(data){       
        var roomname = data;        
        var rooms = io.sockets.manager.rooms;
        console.log("rooms = "+rooms);
        
        if(true ==(roomname in socketRoom)){
          socket.room = data;
          socket.join(data);
        }else{
            socket.room = data;
            socket.join(data);
            jarray[data] = new A();
        }
        
        socketRoom[data] = data;         
        socket.emit('createRoomRES',data);         
    });
    
//creat player
     socket.on("createPlayerREQ", function(data) {
        jarray[socket.room].userNames[socket.id] = data;
        var order = jarray[socket.room].userNum[data] =  jarray[socket.room].thrashNum[jarray[socket.room].thrashIdx];
        jarray[socket.room].thrashIdx--;
        jarray[socket.room].selectCharacter[data] = "random";
        
        if (order % 2 == 0){
            jarray[socket.room].team[data] = "red";		
	} else{
            jarray[socket.room].team[data] = "blue";
        }
        
        var ret;
        ret =  jarray[socket.room].userNum[data]+':'+data;
        io.sockets.in(socket.room).emit("createPlayerRES", ret);
    });
    
    socket.on("preuserREQ", function(data){
        var ret=data+'=';
        
        for(var key in jarray[socket.room].selectCharacter){
           ret +=jarray[socket.room].userNum[key]+':'+key+':'+jarray[socket.room].selectCharacter[key]+":"+jarray[socket.room].team[key]+":"+jarray[socket.room].ready[key]+'-';
        }
        io.sockets.in(socket.room).emit("preuserRES",ret);
    });
    
    socket.on("characterSelectREQ", function(data){
        var temp=data.split(':');
        jarray[socket.room].selectCharacter[temp[0]] = temp[2];
        
        var ret = temp[1]+':'+temp[2];
        io.sockets.in(socket.room).emit("characterSelectRES",ret);
    });
    
    socket.on("readyButtonREQ", function(data){   
        var temp=data.split(':');
        jarray[socket.room].ready[temp[0]] = "true";
        jarray[socket.room].team[temp[0]] = temp[1];
        
        io.sockets.in(socket.room).emit("readyButtonRES",data);
    });
    
    socket.on("startCountREQ", function(data){
         var retPre="";
        
        for(var key in jarray[socket.room]["userNames"]){
            var id =  jarray[socket.room].userNames[key];
            var character = jarray[socket.room].selectCharacter[id];
            if(character == "random"){
                var num = Math.ceil(Math.random() * 4);
                switch(num){
                    case 1:character="furfur";break;
                    case 2:character="barbas";break;
                    case 3:character="guci";break;
                    case 4:character="stola";break;
                }
            }
            retPre += id+":"
             +character+":"
             +jarray[socket.room].team[id]+"_";
        }
        
        io.sockets.in(socket.room).emit("startCountRES",retPre);
    });
    
    socket.on('disconnect',function(data){
        var rooms = io.sockets.manager.rooms;
        var key = socket.room;
        
        if(key!==null){//if client did enter the room
             key = '/'+key;
            if(rooms[key]!=undefined){
            if(rooms[key].length<=1){
                delete(jarray[socket.room]);
                delete(socketRoom[socket.room]);
            }else{
                var ret = jarray[socket.room].userNum[jarray[socket.room].userNames[socket.id]];
                jarray[socket.room].thrashIdx++;
                jarray[socket.room].thrashNum[jarray[socket.room].thrashIdx] = ret;
                io.sockets.in(socket.room).emit("imoutRES", ret);
                
                delete(jarray[socket.room].userNum[jarray[socket.room].userNames[socket.id]]);
                delete(jarray[socket.room].selectCharacter[jarray[socket.room].userNames[socket.id]]);               
                delete(jarray[socket.room].userNames[socket.id]);
            }
            socket.leave(key);
        }
        }
    });
});


