const path = require('path');
const webpack = require('webpack');

const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const CleanWebpackPlugin = require('clean-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin')
const TSLintPlugin = require('tslint-webpack-plugin');

const outputPath = path.resolve(__dirname, 'wwwroot');
const clientPath = './ClientApp';

module.exports = function (env, argv) {

    const isDevelopmentMode = argv.mode === 'development';

    const config = {
        entry: {
            vendor: path.resolve(clientPath, 'vendor.js'),
            app: path.resolve(clientPath, 'app', 'app.ts')
        },
        devtool: 'inline-source-map',
        output: {
            filename: 'js/[name].[hash].js',
            path: outputPath
        },
        plugins: [
            new webpack.ProvidePlugin({
                $: "jquery",
                jQuery: "jquery"
            }),
            new CleanWebpackPlugin([
                'css/*.*',
                'js/*.*',
                'images/*.*',
                'favicon.ico'
            ], {
                    root: outputPath
                }),
            new CopyWebpackPlugin([
                { from: path.resolve(clientPath, 'assets', 'images'), to: path.resolve(outputPath, 'images'), ignore: ['favicon.ico'] },
                { from: path.resolve(clientPath, 'assets', 'images', 'favicon.ico'), to: outputPath }
            ]),
            new MiniCssExtractPlugin({
                filename: "css/[name].[hash].css",
                chunkFilename: "[name].[hash].css"
            }),
            new TSLintPlugin({
                files: [path.resolve(clientPath, '**/*.ts')],
                format: 'prose',
                force: false
            })
        ],
        module: {
            rules: [
                {
                    test: /\.css$/,
                    use: [
                        'style-loader',
                        MiniCssExtractPlugin.loader,
                        {
                            loader: 'css-loader',
                            options: {
                                sourceMap: true,
                                minimize: !isDevelopmentMode
                            }
                        }
                    ]
                },
                {
                    test: /\.(png|woff|woff2|eot|ttf|svg)$/,
                    use: [{
                        loader: 'url-loader',
                        options: {
                            limit: 100000
                        }
                    }]
                },
                {
                    test: /\.scss$/,
                    use: [{
                        loader: "style-loader"
                    }, MiniCssExtractPlugin.loader, {
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
                    test: /\.ts$/,
                    use: [
                        {
                            loader: 'babel-loader',
                            options: {
                                babelrc: true
                            }
                        },
                        'ts-loader'
                    ],
                    exclude: /node_modules/
                },
                {
                    test: /\.js$/,
                    exclude: [
                        /node_modules/,
                    ],
                    use: {
                        loader: 'babel-loader',
                        options: {
                            babelrc: true
                        }
                    }
                }
            ]
        },
        resolve: {
            extensions: ['.ts', '.js']
        }
    }

    return config;
}