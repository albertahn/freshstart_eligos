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
    this.buildingHP={};
    this.timer1;
    this.timer2;
}

io.sockets.on('connection', function (socket) {     

    socket.on("createRoomREQ",function(data){
        var rooms = io.sockets.manager.rooms;
        var temp = data.split(':');

        if(temp[2] =="master"){
            
             socket.room = temp[1];
             socket.join(temp[1]);
             socketRoom[socket.room] = temp[1];             
            jarray[socket.room] = new A();
            
             jarray[socket.room] = socketRoom;       
        socket.emit("youMaster",temp[0]);        
         socket.emit('createRoomRES',temp[0]);
         createMinion();
             }else{
                     console.log("not master");      
                    socket.room = temp[1];
                    socket.join(temp[1]);
                    socket.emit('createRoomRES',temp[0]);                   
             }

 function createMinion(){
             jarray[socket.room].timer1 =  setInterval(redSender,5000);
             jarray[socket.room].timer2 =  setInterval(blueSender,5000);
             
            var maxMinion = 10;
            var currMinion= 0;
            var redIdx=0;
            var blueIdx=0;
            
            function redSender(){
                redIdx++;
                var data = "29.0,50.0,30.0";
                minionNames["rm"+redIdx] = "rm"+redIdx;
                minionPos["rm"+redIdx] = data;
                
               jarray[socket.room].minionNames["rm"+redIdx] = "rm"+redIdx;
               jarray[socket.room].minionPos["rm"+redIdx] = data;
                
                data = "rm"+redIdx+":"+data;         
                 io.sockets.in(socket.room).emit("createRedMinionRES",data);
                 currMinion++;
                 if(currMinion>=maxMinion){
                      clearInterval( jarray[socket.room].timer1  );
                 }
                    
            }
            
            function blueSender(){
                blueIdx++;
                var data = "75.0,50.0,70.0";
                
                jarray[socket.room]["minionNames"]["bm"+blueIdx] = "bm"+blueIdx;
                jarray[socket.room]["minionPos"]["bm"+blueIdx] = data;

                data = "bm"+blueIdx+":"+data;                
                 io.sockets.in(socket.room).emit("createBlueMinionRES",data);
                 currMinion++;
                 if(currMinion>=maxMinion){
                        clearInterval( jarray[socket.room].timer2 );
                 }
                    
            }
        }//end create minion        
    });
//end create room    
    
//crate player
    socket.on("createPlayerREQ", function(data) {
        var retCreP = data.split(":");
 if(JSON.stringify(socket.room) !==undefined){
console.log("jarray[socket.room] ="+jarray[socket.room]);

console.log("jarray[socket.room].userNames ="+jarray[socket.room].userNames);

console.log("jarray[socket.room].userNames[socket.id] ="+jarray[socket.room].userNames[socket.id]);
   // console.log("shit yeso room!: "+jarray+": [socket.id]: "+socket.id +"myid retCreP[0]:  "+ retCreP[0] );
            //1. add username to player in room 
                jarray[socket.room].userNames[socket.id] = retCreP[0];//[socket.id] = ""+ retCreP[0];
            
       //2. add userpos to player in room 

                jarray[socket.room].userPos[retCreP[0]] = retCreP[1];

             //3. add username to player in room 
            
                jarray[socket.room].userCharacter[retCreP[0]] = retCreP[2];
            
            
             //4. add username to player in room 
            
                jarray[socket.room].userTeam[retCreP[0]]=retCreP[3];
            
          //  console.log("afer create jarray: "+ JSON.stringify(jarray));
          
      //  console.log("jarray now : "  + util.inspect(jarray));
          

    }else{//no roomyet
        
//console.log("fuck no room!");

    }
//end
});  //end create player
    
    socket.on("moveMinionREQ",function(data){  
        if(data !==null){
            io.sockets.in(socket.room).emit("moveMinionRES", data);
        }
    });
//    
    socket.on("preuserREQ", function(data){
        var retPre1=data+'=';
        var retPre2=data+'=';
       console.log("socket.room: " + socket.room);
       // console.log("jarray[socket.room] = "+JSON.stringify(jarray[socket.room]));
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

//console.log("ret1: "+ retPre1);

//console.log("ret2: "+ retPre2);

    });
//    
    socket.on("movePlayerREQ",function(data){   
        var retMoveP = data.split(":");
        //typeof myVar != 'undefined'
        if(typeof (jarray[socket.room]["userPos"][retMoveP[0]]) !==undefined){
             jarray[socket.room]["userPos"][retMoveP[0]] = retMoveP[2];
            
        
        io.sockets.in(socket.room).emit("movePlayerRES", data);  

        console.log("move deffinced"+data);


        }else{
            
 console.log("move not deffined"+data);

        }
    });
    
    socket.on("attackREQ",function(data){
        io.sockets.in(socket.room).emit("attackRES", data); 
    });
    
    socket.on("moveSyncREQ",function(data){  
        var ret = data.split(":");
        jarray[socket.room]["userPos"][ret[0]] = ret[1];         
        io.sockets.in(socket.room).emit("moveSyncRES", data);
    });
    
    socket.on("minionAttackREQ",function(data){
        io.sockets.in(socket.room).emit("minionAttackRES", data); 
    });
    
    socket.on("minionSyncREQ",function(data){  
        if(data !==null){//edit?   
            io.sockets.in(socket.room).emit("minionSyncRES", data);
        }
    });
    
////disconnect    
    socket.on('disconnect',function(data){
        var rooms = io.sockets.manager.rooms;
        var key = socket.room; 
        
        if(key!==null){//if client did enter the room


        key = '/'+key;
       // console.log("jarray.userNames: ="+jarray.userNames);
        if(jarray.userNames <=1){

console.log("less than1");
        
                jarray[socket.room].minionPos={};              
                jarray[socket.room].minionNames={};
                 jarray[socket.room].userNames={};
          
                 jarray[socket.room].userPos={};
              
               // jarray[socket.room].minionNames = "undefined";
                        
            clearInterval(jarray[socket.room].timer1);
            clearInterval(jarray[socket.room].timer2);
    
        }else{

           console.log("more than 1");

            var ret = jarray[socket.room].userNames[socket.id];
            io.sockets.in(socket.room).emit("imoutRES", ret);
            jarray[socket.room].userPos[jarray[socket.room].userNames[socket.id]]="";
            jarray[socket.room].userNames[socket.id]="";
                        //delete(socket.room);
            socket.leave(key);



                jarray[socket.room].minionPos={};              
                jarray[socket.room].minionNames={};
                 jarray[socket.room].userNames={};
          
                 jarray[socket.room].userPos={};

            clearInterval(jarray[socket.room].timer1);
            clearInterval(jarray[socket.room].timer2);

            } 
        }
    });


    socket.on('attackMinion', function(data){
        var retAttackM = data.split(":");
        
        buildingHP[retAttackM[0]] = retAttackM[1];         
        io.sockets.in(socket.room).emit("attackMinion", data);
    });

    socket.on('attackBuilding', function(data){
        var retAttackM = data.split(":");
        
        buildingHP[retAttackM[0]] = retAttackM[1];         
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