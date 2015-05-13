var io = require("socket.io").listen(8080);

io.configure(function(){  
    io.set('log level', 2);
});



 var jarray ={  //everything goes here
     
 };

var socketRoom = {};
/*
var userNames = {};//id
var selectCharacter={};
var userNum={};//position in waitRoom
*/

var confirmReady= new Array(6);
for(var i=0;i<=5;i++){
    confirmReady[i]=false;
}

var thrashNum =[];//shoud be at the each room
var thrashIdx=0;//shoud be at the each room

  thrashNum[0]=5;thrashNum[1]=2;thrashNum[2]=4;
  thrashNum[3]=1;thrashNum[4]=3;thrashNum[5]=0;
  thrashIdx=5;

io.sockets.on('connection', function (socket) {       
    console.log("A user connected !");    
    
    
//join room
    socket.on("joinRoomREQ",function(data){
        
        
        var roomname = data;        
        var rooms = io.sockets.manager.rooms;
        for(var key in rooms){
             if(key==''){
                 continue;
             }
             
             if(rooms[key].length<6){//next man
                var roomKey = key.replace('/','');
               
                 socket.room = data;
                 socket.join(data);
                 var data2 = roomname;
                
                socket.emit("createRoomRES", data2);
                socketRoom[socket.room] = roomname;
               
                console.log("room join new :  = "+roomname);
                return;
             }
         }
         
         /*socket.join(socket.id);//first man
         socketRoom[roomname] = roomname;
         data = roomname;
         socket.emit('createRoomRES',data);
          console.log("room join new :  = "+roomname);*/
        
        if(jarray[data] ===undefined){
             
             socket.room = data;
             socket.join(data);
             socketRoom[socket.room ] = data;
             
             
             
             jarray[socket.room ] = socketRoom;
             
             console.log("remade room: "+jarray[socket.room ]);
             
         }else{
             
              socket.room = data;
              socket.join(data);
             
             console.log("made room already " +JSON.stringify(jarray));
         }
         
         console.log("made room already " +JSON.stringify(jarray));
         
        socket.emit('createRoomRES',data); 
        
    });
    
//creat player
     socket.on("createPlayerREQ", function(data) {// data == id 
         var userNames = {};//id
            var selectCharacter={};
            var userNum={};//position in waitRoom
         
        userNames[socket.id] =data;
        userNum[data] = thrashNum[thrashIdx]
        thrashIdx--;
        
        selectCharacter[data] = 'random';
        
//check if there
//1. username
        if(jarray[socket.room]["userNames"]===undefined ){

                jarray[socket.room]["userNames"] = userNames;
                
            }else{
                jarray[socket.room]["userNames"][socket.id] = data;
            }
            
 //2. user char          
            if(jarray[socket.room]["selectCharacter"]===undefined ){

                jarray[socket.room]["selectCharacter"] = selectCharacter;
                
            }else{
                jarray[socket.room]["selectCharacter"][data] = 'random';
            }
 //3. user num           
            if(jarray[socket.room]["userNum"]===undefined ){

                jarray[socket.room]["userNum"] = userNum;
                
            }else{
                jarray[socket.room]["userNum"][socket.id] = userNum;
            }
        
        data = userNum[data]+':'+data;
        io.sockets.in(socket.room).emit("createPlayerRES", data);
        console.log("create : "+ JSON.stringify(jarray));
    }); //create player req
    
    socket.on("preuserREQ", function(data){
        var ret=data+'=';
        
        for(var key in jarray[socket.room]["selectCharacter"]){
            ret +=jarray[socket.room].userNum[key]+':'+key+':'+jarray[socket.room].selectCharacter[key]+'-';

            console.log(jarray[socket.room].selectCharacter[key]);

        }
        
        io.sockets.in(socket.room).emit("preuserRES",ret);
        console.log("preuser REQ");
    });
    
    socket.on("characterSelectREQ", function(data){
        var temp=data.split(':');
        //userNum[temp[0]] = temp[1];
        jarray[socket.room].selectCharacter[temp[0]] = temp[2];    
                
        var ret = temp[1]+':'+temp[2];        
        io.sockets.in(socket.room).emit("characterSelectRES",ret);
    });
    
     socket.on("readyREQ", function(data){
        confirmReady[data]= true;
        console.log("yaya!");
        
        var red=0;
        var blue=0;
        for(var key in confirmReady){
            console.log(key +" = " + confirmReady[key]);
            if(confirmReady[key]==true){
                if(0<=key&&key<=2){
                    red+=1;
                }else if(3<=key&&key<=5){
                    blue+=1;
                }
            }
        }
        console.log("red = "+red);
        console.log("blue = "+blue);
        if(red==blue&&red!=0){
            io.sockets.in(socket.room).emit("readyRES",data);
            console.log("gogogo!");
        }
    });
    
    socket.on('disconnect',function(data){
        var rooms = io.sockets.manager.rooms;
        var key = socket.room;
        
        if(key!==null){//if client did enter the room
             key = '/'+key;
            if(rooms[key].length<=1){
                for(var i in socketRoom){                
                    delete(socketRoom[i]);
                 }
                 for(var i in jarray[socket.room].userNames){
                    delete(jarray[socket.room].userNames[i]);                     
                }
                 for(var i in jarray[socket.room].selectCharacter){
                    delete(jarray[socket.room].selectCharacter[i]);                     
                }
                for(var i in jarray[socket.room].userNum){
                    delete(jarray[socket.room].userNum[i]);                    
                } 
                for(var i=0;i<=5;i++){
                     confirmReady[i]=false;
                }
                
                
  thrashNum[0]=5;thrashNum[1]=2;thrashNum[2]=4;
  thrashNum[3]=1;thrashNum[4]=3;thrashNum[5]=0;
  thrashIdx=5;
            }else{
                var ret = jarray[socket.room].userNum[jarray[socket.room].userNames[socket.id]];
                thrashIdx++;
                thrashNum[thrashIdx] = ret;
                io.sockets.in(socket.room).emit("imoutRES", ret);                
                delete(jarray[socket.room].userNum[jarray[socket.room].userNames[socket.id]]);
                delete(jarray[socket.room].selectCharacter[jarray[socket.room].userNames[socket.id]]);
               
                delete(jarray[socket.room].userNames[socket.id]);
                
                console.log(socket.room);
                
                 delete(socket.room);
                
            }
            socket.leave(key);
        }
    });
});


