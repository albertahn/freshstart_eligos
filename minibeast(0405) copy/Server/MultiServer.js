var io = require("socket.io").listen(8000);

io.configure(function(){  
    io.set('log level', 2);
});

var socketRoom = {};

var buildingHP = {};

var isRun = false;

//var timer1;
//var timer2;



 var jarray ={
     
 };
 

io.sockets.on('connection', function (socket) {     
    //socket.emit('news', { hello: 'world' });      

    console.log("A user connected !");
    
    socket.on("createRoomREQ",function(data){
        var temp = data.split(':');
         var rooms = io.sockets.manager.rooms;
         
         console.log("room_mng: "+ JSON.stringify(rooms));
         
         //if(rooms["/"+temp[1]]==)
         
         for(var key in rooms){
             if(key==''){
                 //create room
                 
                 console.log("no keys :"+key);
                 continue;
                 
             }else{
                 
                 console.log("key: "+key);
                var roomKey = key.replace('/','');
                if(temp[1] == roomKey){
                    
                    console.log("rooms already exits :"+roomKey);
                    socket.room = temp[1];
                    socket.join(temp[1]);
                    socket.emit('createRoomRES',temp[0]);
                    //socketRoom[socket.room] = roomKey;
                  //  console.log("i'm not first socket.id = "+JSON.stringify(jarray));
                    
                    
                    
                    return;
                }
             }
         }//end for loop
       
       
           
         if( typeof jarray[temp[1]]=='undefined'){
           
             socket.room = temp[1];
             socket.join(temp[1]);
             socketRoom[socket.room ] = temp[1];
             
             console.log("remade room: "+jarray[socket.room ]);
             
             jarray[socket.room ] = socketRoom;     
             
         }else{
             
             console.log("made room already " +socket.room);
         }
        
         
         socket.emit('createRoomRES',temp[0]);
       
       // console.log(" "+JSON.stringify(jarray));
         
  
         socket.emit("youMaster",temp[0]);
         createMinion();
        
         function createMinion(){
             var minionNames={};
             var minionPos = {};
         if(typeof (jarray[socket.room].timer1) !='undefined'){
             jarray[socket.room].timer1 = setInterval(redSender,15000);
         }else{
             jarray[socket.room].timer1 = {"timer1":"timer1"};
             
         }    
         
         if(typeof (jarray[socket.room].timer2) !='undefined'){
             jarray[socket.room].timer2 = setInterval(blueSender,15000);
         }else{
             jarray[socket.room].timer2 = {"timer2":"timer2"};
             
         }   
           
             
             
             
            var maxMinion =10;
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
                //console.log("data = "+data);
                 io.sockets.in(socket.room).emit("createRedMinionRES",data);
                 currMinion++;
                 if(currMinion>=maxMinion)
                    clearInterval(jarray[socket.room].timer1 );
                
                // console.log(" "+JSON.stringify(jarray));
            }
            
            function blueSender(){
                blueIdx++;
                var data = "75.0,50.0,70.0";
                minionNames["bm"+blueIdx] = "bm"+blueIdx;
                minionPos["bm"+blueIdx] = data;
                
                jarray[socket.room]["minionNames"]= minionNames;
               
               jarray[socket.room]["minionPos"]= minionPos;
                
                // console.log(" "+JSON.stringify(jarray));
                
                data = "bm"+blueIdx+":"+data;                
                 io.sockets.in(socket.room).emit("createBlueMinionRES",data);
                 currMinion++;
                 if(currMinion>=maxMinion)
                    clearInterval(jarray[socket.room].timer2);
            }
        }//end create minion
        
          //console.log("minionnames "+JSON.stringify(jarray[socket.room]["minionNames"]));
        
    });//end create room
    
    

    socket.on("createPlayerREQ", function(data) {
        
        
var userNames = {};
var userPos = {};
var userCharacter = {};
var userTeam={};
        
        var ret = data.split(":");
        
        io.sockets.in(socket.room).emit("createPlayerRES", data);
        
        userNames[socket.id] = ret[0];        
    userPos[ret[0]] = ret[1];
        userCharacter[ret[0]] = ret[2];
        userTeam[ret[0]] = ret[3];
        
         console.log("my room : "+ JSON.stringify(socket.room));
         
       // console.log("create playername jaray: "+ JSON.stringify(jarray[socket.room]["userNames"]));
 if(JSON.stringify(socket.room) !==undefined){
        
            //1. add username to player in room 
            if(JSON.stringify(jarray[socket.room]["userNames"])===undefined ){

                jarray[socket.room]["userNames"] = userNames;
            }else{
                jarray[socket.room]["userNames"][socket.id] = ret[0];
            }
        //2. add userpos to player in room 
            if(jarray[socket.room]["userPos"]===undefined ){

                jarray[socket.room]["userPos"] = userPos;
                
            }else{
                
                jarray[socket.room]["userPos"][ret[0]] = ret[1];
            }
            
             //3. add username to player in room 
            if(jarray[socket.room]["userCharacter"]===undefined ){

                jarray[socket.room]["userCharacter"] = userCharacter;
            }else{
                jarray[socket.room]["userCharacter"][ret[0]] = ret[2];
            }
            
             //4. add username to player in room 
            if(jarray[socket.room]["userTeam"]===undefined ){

                jarray[socket.room]["userTeam"] = userTeam;
            }else{
                jarray[socket.room]["userTeam"][ret[0]]=ret[3];
            }
            
            
            
                     //console.log("createplayer: ret"+ret[0]);
  console.log("room: "+JSON.stringify(socket.room));
 console.log("username: "+JSON.stringify(jarray[socket.room].userNames));
 console.log("userPos: "+JSON.stringify(jarray[socket.room].userPos));
 console.log("userCharacter: "+JSON.stringify(jarray[socket.room].userCharacter));
 console.log("userTeam: "+JSON.stringify(jarray[socket.room].userTeam));
      //   console.log("the array :"+JSON.stringify(jarray));
    }else{//no roomyet
        
         console.log("no room : "+ JSON.stringify(socket.room));
    }
//end

 
    });
    
    socket.on("moveMinionREQ",function(data){  
        if(data !==null){
            io.sockets.in(socket.room).emit("moveMinionRES", data);
        }
    });
    
    socket.on("preuserREQ", function(data){
        var ret1=data+'=';
        var ret2=data+'=';
       
        for(var key in jarray[socket.room]["minionNames"]){
            ret1 += jarray[socket.room].minionNames[key]+":"
                    +jarray[socket.room]["minionPos"][jarray[socket.room].minionNames[key]]
                    +"_";
        }
        
        for(var key in jarray[socket.room]["userNames"]){
            ret2 += jarray[socket.room]["userNames"][key]+":"
             +jarray[socket.room].userPos[jarray[socket.room].userNames[key]]+":"
             +jarray[socket.room].userCharacter[jarray[socket.room].userNames[key]]
             +":"+jarray[socket.room].userTeam[jarray[socket.room].userNames[key]]+"_";
        }
        io.sockets.in(socket.room).emit("preuser1RES",ret1);
        io.sockets.in(socket.room).emit("preuser2RES",ret2);
    });
    
    socket.on("movePlayerREQ",function(data){   
        var ret = data.split(":");
        
        //typeof myVar != 'undefined'
        if(typeof (jarray[socket.room]["userPos"][ret[0]]) !==undefined){
            
            console.log(socket.room+":"+ret[0]+ ": movePlayerREQ : "+jarray[socket.room]["userPos"][ret[0]]);
            
        jarray[socket.room]["userPos"][ret[0]] = ret[1];
        io.sockets.in(socket.room).emit("movePlayerRES", data);      
        }else{
            
            console.log(socket.room+" undefined : movePlayerREQ error: "+JSON.stringify(jarray[socket.room]));
        }
        
        
        
       // console.log(ret[0] +": "+ret[1]);
    });
    
    socket.on("attackREQ",function(data){
        io.sockets.in(socket.room).emit("attackRES", data); 
    });
    
    socket.on("moveSyncREQ",function(data){  
        var ret = data.split(":");
        jarray[socket.room]["userPos"][ret[0]] = ret[1];         
        io.sockets.in(socket.room).emit("moveSyncRES", data);
        
      //  console.log("ret: "+ ret[0]);
        
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
        console.log("key = "+key);
        if(rooms[key].length <=1){
            
            
            for(var i in jarray[socket.room].minionPos){
                delete(jarray[socket.room].minionPos[i]);              
            }
            
            for(var i in jarray[socket.room].userNames){               
                 delete(jarray[socket.room].userNames[i]);
            }
            
                for(var i in jarray[socket.room].userPos){               
                 delete(jarray[socket.room].userPos[i]);
            }

            for(var i in jarray[socket.room].minionNames){
                delete(jarray[socket.room].minionNames[i]);
            }
                        
            clearInterval(jarray[socket.room].timer1);
            clearInterval(jarray[socket.room].timer2);

            delete (jarray[socket.room]);
            
        }else{
            

                 var ret = jarray[socket.room].userNames[socket.id];
                        io.sockets.in(socket.room).emit("imoutRES", ret);
                        delete(jarray[socket.room].userPos[jarray[socket.room].userNames[socket.id]]);
                        delete(jarray[socket.room].userNames[socket.id]);
                        delete(socket.room);
                        socket.leave(key);

        } 
            
       
        }
    });


    socket.on('attackMinion', function(data){
        var ret2 = data.split(":");
        
        buildingHP[ret2[0]] = ret2[1];         
        io.sockets.in(socket.room).emit("attackMinion", data);
    });

    socket.on('attackBuilding', function(data){
        var ret2 = data.split(":");
        
        buildingHP[ret2[0]] = ret2[1];         
        io.sockets.in(socket.room).emit("attackBuilding", data);
    });

    socket.on('SkillAttack', function(data){
            
       io.sockets.in(socket.room).emit("SkillAttack", data);
    });
    
    
    socket.on('HealthSync', function(data){
            
       io.sockets.in(socket.room).emit("HealthSync", data);
    });



});