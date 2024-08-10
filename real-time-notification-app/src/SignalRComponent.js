import React, { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";

const SignalRComponent = () => {
  const [message, setMessage] = useState("");

  useEffect(() => {
    const userId = "1"; // Bu, istemcinin ID'si (örneğin oturum açmış kullanıcının ID'si)
    
    // SignalR bağlantısını başlatma
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(`https://localhost:7270/notificationHub?userId=${userId}`, {
        withCredentials: false
      }) // Hub endpoint URL'inizi burada belirtin
      .configureLogging(signalR.LogLevel.Debug)
      .withAutomaticReconnect()
      .build();

      connection.onreconnecting((error) => {
        console.warn("SignalR yeniden bağlanıyor:", error);
      });
      
      connection.onreconnected(() => {
        console.log("SignalR yeniden bağlandı");
      });
      
      connection.onclose((error) => {
        console.error("SignalR bağlantısı kapandı:", error);
      });
      
    connection.start()
      .then(() => {
        console.log("SignalR bağlantısı açıldı.");

        // Sunucudan gelen mesajları dinle
        connection.on("ReceiveNotification", (message) => {
          console.log("Message received in client:", message); // Gelen mesajı loglayın
          setMessage(message); // State'i güncelleyin
        });
      })
      .catch(err => console.error("SignalR bağlantısı başarısız oldu:", err));

    return () => {
      connection.stop();
    };
  }, []);

  return (
    <div>
      <h2>SignalR'dan Gelen Mesaj:</h2>
      <p>{message}</p>
    </div>
  );
};

export default SignalRComponent;
