import "./globals.css";

import { Inter } from "next/font/google";
import type { Metadata } from "next";

const inter = Inter({ subsets: ["latin"] });

export const metadata: Metadata = {
	title: "Lucra Image Gallery",
	description: "A public facing image gallery for all by Lucra",
};

export default function RootLayout({
	children,
}: {
	children: React.ReactNode;
}) {
	return (
		<html lang="en">
			<body className={inter.className}>
				<main className="max-w-5xl mx-auto p-3">{children}</main>
			</body>
		</html>
	);
}
