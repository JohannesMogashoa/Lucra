/** @type {import('next').NextConfig} */
const nextConfig = {
    images: {
        remotePatterns: [
            {
                protocol: 'http',
                hostname: 'localhost',
                port: '5054'
            },
            {
                protocol: 'https',
                hostname: 'localhost',
                port: '7210'
            }
        ]
    }
}

module.exports = nextConfig
