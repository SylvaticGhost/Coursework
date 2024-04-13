'use client';

import { useEffect, useState } from "react";
import Cookies from "js-cookie";

export default function LoginOrProfileButton() {
  const [logged, setLogged] = useState<boolean | undefined>(undefined);
  const [token, setToken] = useState<string | undefined>(undefined);

  useEffect(() => {
    setLogged(Cookies.get('logged') === 'true');
    setToken(Cookies.get('token'));
    
    function handleCookieChange() {
      setLogged(Cookies.get('logged') === 'true');
      setToken(Cookies.get('token'));
      
      if(!token)
        throw new Error('Token is not set')
      
      console.log('token: ' + token)
    }

    window.addEventListener('storage', handleCookieChange); // Listen for changes in the storage

    // Cleanup
    return () => {
      window.removeEventListener('storage', handleCookieChange);
    };
  }, []); // Empty dependency array means this effect runs once on mount and cleanup on unmount

  return (
    <a href={token ? 'http://localhost:3000/Profile/MyProfile' : 'http://localhost:3000/Auth/login'} className="flex ">
      {token ? 'Profile' : 'Login'}
    </a>
  );
}