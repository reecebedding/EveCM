const path = require('path');
const webpack = require('webpack');

const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const CleanWebpackPlugin = require('clean-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin')
const SpeedMeasurePlugin = require('speed-measure-webpack-plugin');

const outputPath = path.resolve(__dirname, 'wwwroot');
const clientPath = './ClientApp';

function GetPathToComponentEntryFile(parentDir, fileName) {
    return path.resolve(clientPath, 'app', 'Components', parentDir, fileName);
}

module.exports = function (env, argv) {

    const isDevelopmentMode = argv.mode === 'development';

    const smp = new SpeedMeasurePlugin();

    const config = smp.wrap({
        entry: {
            vendor: path.resolve(clientPath, 'vendor.js'),
            global: path.resolve(clientPath, 'app', 'global.ts'),
            
            home: GetPathToComponentEntryFile('Home', 'homePage.tsx'),
            admin: GetPathToComponentEntryFile('Admin', 'adminPage.tsx')
        },
        devtool: isDevelopmentMode ? 'inline-source-map' : '',
        output: {
            filename: isDevelopmentMode ? 'js/[name].js' : 'js/[name].[hash].js',
            path: outputPath
        },
        plugins: [
            new webpack.ProvidePlugin({
                $: "jquery",
                jQuery: "jquery"
            }),
            new CleanWebpackPlugin([
                '*'
            ], {
                    exclude: [],
                    root: outputPath
                }),
            new CopyWebpackPlugin([
                { from: path.resolve(clientPath, 'assets', 'images'), to: path.resolve(outputPath, 'images'), ignore: ['favicon.ico'] },
                { from: path.resolve(clientPath, 'assets', 'images', 'favicon.ico'), to: outputPath }
            ]),
            new MiniCssExtractPlugin({
                filename: isDevelopmentMode ? 'css/[name].css' : 'css/[name].[hash].css',
                chunkFilename: isDevelopmentMode ? '[name].css' : '[name].[hash].css'
            })
        ],
        module: {
            rules: [
                {
                    test: /\.(png|woff|woff2|eot|ttf|svg)$/,
                    use: [{
                        loader: 'url-loader',
                        options: {
                            limit: 100000,
                            name: 'fonts/[hash].[ext]'
                        }
                    }]
                },
                {
                    test: /\.scss$/,
                    use: [
                        MiniCssExtractPlugin.loader,
                        {
                            loader: "css-loader", options: {
                                sourceMap: true
                            }
                        }, {
                            loader: "sass-loader", options: {
                                sourceMap: true,
                                outputStyle: isDevelopmentMode ? "nested" : "compressed"
                            }
                        }]
                },
                {
                    test: /\.tsx?$/,
                    use: [{
                        loader: 'babel-loader',
                        options: {
                            babelrc: true
                        }
                    },{
                        loader: 'tslint-loader'
                    }],
                    exclude: /node_modules/
                }
            ]
        },
        resolve: {
            extensions: ['.ts', '.tsx', '.js', '.jsx', '.json']
        }
    });

    return config;
}