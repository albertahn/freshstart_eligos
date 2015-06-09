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
    this.minionNames = {};
    this.minionPos = {};
    this.timer1 = {};
    this.timer2 = {};
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
        //  createMinion();
        }
        socket.emit('createRoomRES',temp[0]);

        function createMinion(){            
            jarray[socket.room].timer1 =  setInterval(redSender,10000);
            jarray[socket.room].timer2 =  setInterval(blueSender,10000);
            
            var redIdx=0;
            var blueIdx=0;
            
            function redSender(){
                redIdx++;
                var data = "29.0,50.0,30.0";
                console.log("jarray[socket.room] = "+jarray[socket.room]);
                console.log(" jarray[socket.room].minionNames = "+ jarray[socket.room].minionNames);
                jarray[socket.room].minionNames["rm"+redIdx] = "rm"+redIdx;
                jarray[socket.room].minionPos["rm"+redIdx] = data;
                
                data = "rm"+redIdx+":"+data;         
                 io.sockets.in(socket.room).emit("createRedMinionRES",data);          
            }
            
            function blueSender(){
                blueIdx++;
                var data = "75.0,50.0,70.0";
                
                jarray[socket.room]["minionNames"]["bm"+blueIdx] = "bm"+blueIdx;
                jarray[socket.room]["minionPos"]["bm"+blueIdx] = data;

                data = "bm"+blueIdx+":"+data;                
                 io.sockets.in(socket.room).emit("createBlueMinionRES",data);       
            }
        }//end create minion        
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
        var retPre1=data+'=';
        var retPre2=data+'=';
        
        for(var key in jarray[socket.room]["minionNames"]){
            retPre1 += jarray[socket.room]["minionNames"][key]+":"
                    +jarray[socket.room]["minionPos"][jarray[socket.room]["minionNames"][key]]
                    +"_";
        }
        for(var key in jarray[socket.room]["userNames"]){
            retPre2 += jarray[socket.room]["userNames"][key]+":"
             +jarray[socket.room].userPos[jarray[socket.room].userNames[key]]+":"
             +jarray[socket.room].userCharacter[jarray[socket.room].userNames[key]]
             +":"+jarray[socket.room].userTeam[jarray[socket.room].userNames[key]]+"_";
        }
        
        io.sockets.in(socket.room).emit("preuser1RES",retPre1);
        io.sockets.in(socket.room).emit("preuser2RES",retPre2);
    });

    socket.on("movePlayerREQ",function(data){   
        var retMoveP = data.split(":");        
        jarray[socket.room]["userPos"][retMoveP[0]] = retMoveP[2];            
        
        io.sockets.in(socket.room).emit("movePlayerRES", data);  
    });
    
    socket.on("attackREQ",function(data){
        io.sockets.in(socket.room).emit("attackRES", data); 
    });
    
    socket.on("respawnREQ",function(data){
        io.sockets.in(socket.room).emit("respawnRES", data);
    });
    
   /* socket.on("moveSyncREQ",function(data){  
        var ret = data.split(":");
        jarray[socket.room]["userPos"][ret[0]] = ret[1];         
        io.sockets.in(socket.room).emit("moveSyncRES", data);
    });*/
    
    socket.on("minionAttackREQ",function(data){
        io.sockets.in(socket.room).emit("minionAttackRES", data); 
    });
    
    /*socket.on("minionSyncREQ",function(data){  
        if(data !==null){//edit?   
            io.sockets.in(socket.room).emit("minionSyncRES", data);
        }
    });*/
    
    socket.on('disconnect',function(data){
        var rooms = io.sockets.manager.rooms;
        var key = socket.room; 
        
        if(key!==null){//if client did enter the room
            key = '/'+key;
            if(rooms[key]!=undefined){
            if(rooms[key].length <=1){
                clearInterval(jarray[socket.room].timer1);
                clearInterval(jarray[socket.room].timer2);
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
    
     socket.on('minionDieREQ', function(data){
        io.sockets.in(socket.room).emit("minionDieRES", data);
    });

    socket.on('attackBuilding', function(data){
        io.sockets.in(socket.room).emit("attackBuilding", data);
    });

    socket.on('SkillAttack', function(data){
       io.sockets.in(socket.room).emit("SkillAttack", data);
    });    
    
    socket.on('HealthSync', function(data){
       io.sockets.in(socket.room).emit("HealthSync", data);
    });

     socket.on('cannonDie', function(data){
       io.sockets.in(socket.room).emit("cannonDie", data);
    });
});