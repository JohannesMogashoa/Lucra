import type { Config } from "tailwindcss";

const config: Config = {
	content: [
		"./pages/**/*.{js,ts,jsx,tsx,mdx}",
		"./components/**/*.{js,ts,jsx,tsx,mdx}",
		"./app/**/*.{js,ts,jsx,tsx,mdx}",
	],
	daisyui: {
		themes: ["winter"],
	},
	plugins: [require("@tailwindcss/typography"), require("daisyui")],
};
export default config;
