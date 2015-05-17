var io = require("socket.io").listen(8000);

//get


io.configure(function(){  
    io.set('log level', 2);
});

var socketRoom = {};
var buildingHP = {};

var isRun = false;

var timer1;
var timer2;

 var jarray ={
     
 };
 var minionNames = {};
 var minionPos = {};  



//jaray structure
//var jarray = {};

var jarray ={room1: "room1"} ;//={room7:"{userNames:"", userCharacter:"",userTeam:"",userPos:"",minionNames:"",minionPos:,""}"  };      
jarray.room1 = {userNames:"", userCharacter :"", userTeam:"", userPos:"", minionNames:"", minionPos:""};

jarray.room1.userNames = {};
jarray.room1.userTeam = {};
jarray.room1.userPos = {};
jarray.room1.userCharacter = {}
jarray.room1.minionNames = {};
jarray.room1.minionPos = {};

console.log("jarray now : "
    + JSON.stringify(jarray));


     /*   
jarray.room7.userNames = "hi ther";//{try:"heart"};
jarray.room7.userCharacter = {try:"heart"};
jarray.room7.userTeam = {try:"heart"};
*/




//set the jarray for each room before socket connect




io.sockets.on('connection', function (socket) {     
    //socket.emit('news', { hello: 'world' });    
    
console.log("The JARAY: "+JSON.stringify(jarray));

    socket.on("createRoomREQ",function(data){
        var temp = data.split(':');
         var rooms = io.sockets.manager.rooms;

         socket.room = temp[1];
        socket.join(temp[1]);

        console.log("joing room : "+ socket.room);
         
         //if(rooms["/"+temp[1]]==)
         
         for(var key in rooms){
             if(key==''){
                 //create room
                 continue;
             }else{
                var roomKey = key.replace('/','');
                if(temp[1] == roomKey){
                    socket.room = temp[1];
                    socket.join(temp[1]);
                    socket.emit('createRoomRES',temp[0]);
                    //socketRoom[socket.room] = roomKey;
                    return;
                }
             }
         }//end for loop
           
         if( typeof jarray[temp[1]]=='undefined'){
           
             socket.room = temp[1];
             socket.join(temp[1]);
             socketRoom[socket.room ] = temp[1];
             jarray[socket.room ] = socketRoom;     
             
         }else{
             
         }
        
         
         socket.emit('createRoomRES',temp[0]);
         
  
         socket.emit("youMaster",temp[0]);
         setTimeout(createMinion,1000);
         //createMinion();
        
         function createMinion(){
        /* if(typeof (jarray[socket.room].timer1) !='undefined'){
             jarray[socket.room].timer1 = setInterval(redSender,3000);
         }else{
             jarray[socket.room].timer1 = {"timer1":"timer1"};
             
         }    
         
         if(typeof (jarray[socket.room].timer2) !='undefined'){
             jarray[socket.room].timer2 = setInterval(blueSender,3000);
         }else{
             jarray[socket.room].timer2 = {"timer2":"timer2"};
             
         }*/
             timer1 =  setInterval(redSender,5000);
             timer2 =  setInterval(blueSender,5000);
             
             
            var maxMinion =100000;
            var currMinion=0;
            var redIdx=0;
            var blueIdx=0;
            
            function redSender(){
                redIdx++;
                var data = "29.0,50.0,30.0";
                minionNames["rm"+redIdx] = "rm"+redIdx;
                minionPos["rm"+redIdx] = data;
                
               jarray[socket.room].minionNames= minionNames; 
               jarray[socket.room].minionPos = minionPos;
                
                data = "rm"+redIdx+":"+data;         
                 io.sockets.in(socket.room).emit("createRedMinionRES",data);
                 currMinion++;
                 if(currMinion>=maxMinion)
                    clearInterval(jarray[socket.room].timer1 );
            }
            
            function blueSender(){
                blueIdx++;
                var data = "75.0,50.0,70.0";
                minionNames["bm"+blueIdx] = "bm"+blueIdx;
                minionPos["bm"+blueIdx] = data;
                
                jarray[socket.room]["minionNames"]= minionNames;               
               jarray[socket.room]["minionPos"]= minionPos;
                
                data = "bm"+blueIdx+":"+data;                
                 io.sockets.in(socket.room).emit("createBlueMinionRES",data);
                 currMinion++;
               //  if(currMinion>=maxMinion)
               //     clearInterval(jarray[socket.room].timer2);
            }
        }//end create minion        
    });
//end create room    
    

    socket.on("createPlayerREQ", function(data) {
var userNames = {};
var userPos = {};
var userCharacter = {};
var userTeam={};
        
        var retCreP = data.split(":");
        
        io.sockets.in(socket.room).emit("createPlayerRES", data);
        
        userNames[socket.id] = retCreP[0];    //client id      
        userPos[retCreP[0]] = retCreP[1];         //position 
        userCharacter[retCreP[0]] = retCreP[2];    //char
        userTeam[retCreP[0]] = retCreP[3];        //team
 if(JSON.stringify(socket.room) !==undefined){

    console.log("shit yeso room!: "+socket.room+": [socket.id]: "+socket.id +"myid retCreP[0]:  "+ retCreP[0] );

        //jarray[socket.room].userNames

//         jarray[socket.room].userNames[socket.id] = retCreP[0]; //{ socket.id +":"+retCreP[0]};

            //1. add username to player in room 
            
                jarray[socket.room]["userNames"][socket.id] = retCreP[0];
            
        //2. add userpos to player in room 

                jarray[socket.room]["userPos"][retCreP[0]] = retCreP[1];

             //3. add username to player in room 
            
                jarray[socket.room]["userCharacter"][retCreP[0]] = retCreP[2];
            
            
             //4. add username to player in room 
            
                jarray[socket.room]["userTeam"][retCreP[0]]=retCreP[3];
            
            console.log("afer create jarray: "+ JSON.stringify(jarray));

    }else{//no roomyet
        
console.log("fuck no room!");

    }
//end
});
    
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
        console.log("jarray[socket.room] = "+JSON.stringify(jarray[socket.room]));
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
//    
    socket.on("movePlayerREQ",function(data){   
        var retMoveP = data.split(":");
        //typeof myVar != 'undefined'
        if(typeof (jarray[socket.room]["userPos"][retMoveP[0]]) !==undefined){
             jarray[socket.room]["userPos"][retMoveP[0]] = retMoveP[2];
            
        
        io.sockets.in(socket.room).emit("movePlayerRES", data);      
        }else{
            
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
    
    socket.on('disconnect',function(data){
        var rooms = io.sockets.manager.rooms;
        var key = socket.room; 
        
        if(key!==null){//if client did enter the room


        key = '/'+key;
        //console.log("rooms[key].length="+rooms[key].length);
        if(jarray.userNames <=1){           
      /*s      for(var i in jarray[socket.room].minionPos){
                jarray[socket.room].minionPos[i]="";              
            }
            
            for(var i in jarray[socket.room].userNames){               
                 jarray[socket.room].userNames[i]="";
            }
            
                for(var i in jarray[socket.room].userPos){               
                 jarray[socket.room].userPos[i]="";
            }

            for(var i in jarray[socket.room].minionNames){
                //console.log("pre = "+jarray[socket.room].minionNames[i]);
                jarray[socket.room].minionNames.i="";
                //delete(minionNames.i);
                //console.log("after = "+jarray[socket.room].minionNames[i]);
            }
               // jarray[socket.room].minionNames = "undefined";
                        
            clearInterval(timer1);
            clearInterval(timer2);

         jarray[socket.room]="";           
        */}else{


           /* var ret = jarray[socket.room].userNames[socket.id];
            io.sockets.in(socket.room).emit("imoutRES", ret);
            jarray[socket.room].userPos[jarray[socket.room].userNames[socket.id]]="";
            jarray[socket.room].userNames[socket.id]="";
                        //delete(socket.room);
            socket.leave(key);
*/

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