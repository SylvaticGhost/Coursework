import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import React from "react";
import LoginOrProfileButton from "@/Components/LoginOrProfileButton";
import WorklyTittle from "@/Components/WorklyTittle";
import { useRouter } from 'next/router';

const url = 'http://localhost:3000';
const inter = Inter({ subsets: ["latin"] });

const loginLink = "http://localhost:3000/Auth/login";

export const metadata: Metadata = {
  title: "WORKLY",
  description: "workly",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
    console.log("RootLayout");
  
    
  return (
      <html lang="en">
      <body>
      <header className="mb-3 py-2 border-b-4 border-b-fuchsia-600 px-1 flex justify-between text-xl">
          <WorklyTittle />
          <LoginOrProfileButton />
      </header>
      <main className="my-24" style={{flexGrow: 1}}>{children}</main>
      <footer className="py-3 mt-3 border-t-4 border-t-fuchsia-600 px-4 bg-fuchsia-500 text-white">
          <div className="flex justify-between  font-semibold text-xl">
              <a href="http://localhost:3000/AppPages/AboutUs">About Us</a>
              <a href={url + '/CompanyHub/CompanyLogin'}>Company Hub</a>
              <a href="http://localhost:3000/CompanyHub/CreateCompany">Want to create company?</a>
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
