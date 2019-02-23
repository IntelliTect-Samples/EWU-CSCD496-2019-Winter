// Lib
const autoprefixer = require('autoprefixer');
const path = require('path');
const webpack = require('webpack');

// Plugins
const CleanWebpackPlugin = require('clean-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = (env, argv) => {
    const isProd = argv.mode === 'production';

    // Paths
    const distPath = path.resolve(__dirname, './wwwroot');
    const imgDistPath = path.resolve(distPath, './img');
    const srcPath = '.';
    const templatePath = path.resolve(__dirname, './Views/Shared');

    return {
        entry: './scripts/main.ts',
        output: {
            filename: 'js/[name].[hash].js',
            path: distPath,
            publicPath: '/',
        },

        module: {
            rules: [
                {
                    test: /\.scss$/,
                    use: [
                        { loader: MiniCssExtractPlugin.loader },
                        { loader: 'css-loader' },
                        { loader: 'postcss-loader', options: { ident: 'postcss', plugins: () => [autoprefixer()], }, },
                        { loader: 'sass-loader' }
                    ],
                }
            ],
        },
        plugins: [
            new CleanWebpackPlugin(
                [
                    '**/*.js',
                    '**/*.css'
                ],
                {
                    root: distPath,
                    watch: true,
                },
            ),

            // Copy src images to wwwroot
            new CopyWebpackPlugin(
                [
                    //{ from: 'favicon', to: distPath },
                    //{ from: 'icons', to: iconDistPath },
                    //{ from: 'images', to: imgDistPath },
                ],
                {
                    context: srcPath,
                    ignore: ['*.DS_Store'],
                },
            ),

            new MiniCssExtractPlugin({
                filename: 'css/[name].[hash].css',
                path: distPath,
                publicPath: '/',
            }),

            new HtmlWebpackPlugin({
                filename: path.resolve(templatePath, './_Layout.cshtml'),
                inject: false,
                minify: false,
                template: path.resolve(templatePath, './_Layout_Template.cshtml'),
            })
        ],
        resolve: {
            extensions: ['.ts', '.tsx', '.js', '.jsx', '.json'],
            modules: [
                path.resolve(__dirname, './node_modules'),
                srcPath,
            ],
        },
    };
}