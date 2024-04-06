import Image from "next/image";
import Cookies from "js-cookie";

export default function Home() {
    console.log("Home");
    const token = Cookies.get('token');
    const logged = Cookies.get('logged') === 'true';
    console.log(token);
    
  return (
    <main>
      <h1>Home</h1>
      
    </main>
  );
}
