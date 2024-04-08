import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import React from "react";
import Link from "next/link";
import Cookies from "js-cookie";
import LoginOrProfileButton from "@/Components/LoginOrProfileButton";
import WorklyTittle from "@/Components/WorklyTittle";

const url = 'http://localhost:3000';
const inter = Inter({ subsets: ["latin"] });

const loginLink = "http://localhost:3000/Auth/login";

export const metadata: Metadata = {
  title: "Create Next App",
  description: "Generated by create next app",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
    console.log("RootLayout");
  const logged: boolean = Cookies.get('logged') === 'true';
  //const id: string | undefined = Cookies.get('id') || undefined;
  
    
  return (
      <html lang="en">
      <body>
      <header className="py-2 border-b-4 border-b-fuchsia-600 px-1 flex justify-between text-xl">
          <WorklyTittle />
          <LoginOrProfileButton />
      </header>
      <main className="my-2" style={{flexGrow: 1}}>{children}</main>
      <footer className="py-3 my-2 border-t-4 border-t-fuchsia-600 px-4 bg-fuchsia-500 text-white">
          <div className="flex justify-between  font-semibold text-xl">
              <a href="http://localhost:3000/AppPages/AboutUs">About Us</a>
              <a href={url + '/CompanyHub/CompanyLogin'}>Company Hub</a>
              <a href="http://localhost:3000/Companies/CreateCompany">Want to create company?</a>
          </div>
          <br/>
          <div>
              <p>Workly is a platform that connects companies with potential employees. We are here to help you find the best job for you.</p>
              <p>2024</p>
          </div>
      </footer>
      </body>
      </html>
  );
}
