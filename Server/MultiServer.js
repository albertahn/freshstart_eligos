var io = require("socket.io").listen(8000);

//get
var util = require('util');

io.configure(function(){  
    io.set('log level', 2);
});

var socketRoom = {};

var jarray = {};

function A(){    
    this.userNames = {};
    this.userTeam = {};
    this.userPos = {};
    this.userCharacter = {};
}

io.sockets.on('connection', function (socket) {

    socket.on("createRoomREQ",function(data){
        var temp = data.split(':');
        
        var roomname = temp[1];
        var rooms = io.sockets.manager.rooms;
        
        if((roomname in socketRoom)){
            socket.room = temp[1];
            socket.join(temp[1]);
        }else{
            socket.room = temp[1];
            socket.join(temp[1]);
            jarray[temp[1]] = new A(); 
        }
        
        socketRoom[temp[1]] = temp[1];

        if(temp[2] =="master"){
          socket.emit("youMaster",temp[0]);
        }
        socket.emit('createRoomRES',temp[0]); 
    });
    
//end create room    
    socket.on("createMinionREQ", function(data) { 
        io.sockets.in(socket.room).emit("createMinionRES", data);
    });
    
//crate playe
    socket.on("createPlayerREQ", function(data) {
        var ret = data.split(":");
    
        io.sockets.in(socket.room).emit("createPlayerRES", data);        
        jarray[socket.room].userNames[socket.id] = ret[0];    //client id      
        jarray[socket.room].userPos[ret[0]] = ret[1];         //position 
        jarray[socket.room].userCharacter[ret[0]] = ret[2];    //char
        jarray[socket.room].userTeam[ret[0]] = ret[3];        //team
    });  //end create player
    
    socket.on("moveMinionREQ",function(data){
        io.sockets.in(socket.room).emit("moveMinionRES", data);
    });
//    
    socket.on("preuserREQ", function(data){
        var retPre=data+'=';
        
        for(var key in jarray[socket.room]["userNames"]){
            retPre += jarray[socket.room]["userNames"][key]+":"
             +jarray[socket.room].userPos[jarray[socket.room].userNames[key]]+":"
             +jarray[socket.room].userCharacter[jarray[socket.room].userNames[key]]
             +":"+jarray[socket.room].userTeam[jarray[socket.room].userNames[key]]+"_";
        }        
        io.sockets.in(socket.room).emit("preuserRES",retPre);
    });

    socket.on("movePlayerREQ",function(data){   
        var retMoveP = data.split(":");
       // jarray[socket.room]["userPos"][retMoveP[0]] = retMoveP[2];            
        
        io.sockets.in(socket.room).emit("movePlayerRES", data);  
    });
    
    socket.on("attackREQ",function(data){
        io.sockets.in(socket.room).emit("attackRES", data); 
    });
    
    socket.on("respawnREQ",function(data){
        io.sockets.in(socket.room).emit("respawnRES", data);
    });
    
    socket.on("minionAttackREQ",function(data){
        io.sockets.in(socket.room).emit("minionAttackRES", data); 
    });
    
    socket.on('disconnect',function(data){
        var rooms = io.sockets.manager.rooms;
        var key = socket.room; 
        
        if(key!==null){//if client did enter the room
            key = '/'+key;
            if(rooms[key]!=undefined){
            if(rooms[key].length <=1){
                delete(jarray[socket.room]);
                delete(socketRoom[socket.room]);
            }else{
                var ret = jarray[socket.room].userNames[socket.id];
                io.sockets.in(socket.room).emit("imoutRES", ret);
                
                delete(jarray[socket.room].userPos[jarray[socket.room].userNames[socket.id]]);
                delete(jarray[socket.room].userNames[socket.id]);
            } 
            socket.leave(key);
            }
        }
    });


    socket.on('attackMinion', function(data){        
        io.sockets.in(socket.room).emit("attackMinion", data);
    });
    
    socket.on('attackCannon', function(data){        
        io.sockets.in(socket.room).emit("attackCannonRES", data);
    });
    
     socket.on('minionDieREQ', function(data){
        io.sockets.in(socket.room).emit("minionDieRES", data);
    });

    socket.on('attackBuilding', function(data){
        io.sockets.in(socket.room).emit("attackBuilding", data);
    });

    socket.on('SkillAttack', function(data){
       io.sockets.in(socket.room).emit("SkillAttack", data);
    });
    
    socket.on('SkillDamageREQ', function(data){
        console.log("skillDAMAGE REQ");
       io.sockets.in(socket.room).emit("SkillDamageRES", data);
    });  
    
    socket.on('HealthSync', function(data){
       io.sockets.in(socket.room).emit("HealthSync", data);
    });
    
       
    socket.on('playerHpSync', function(data){
       io.sockets.in(socket.room).emit("playerHpSyncRES", data);
    });

     socket.on('cannonDie', function(data){
       io.sockets.in(socket.room).emit("cannonDie", data);
    });

     socket.on('statSyncReq', function(data){
       io.sockets.in(socket.room).emit("statSyncRes", data);
    });
});