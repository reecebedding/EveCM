const path = require('path');
const webpack = require('webpack');

const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const CleanWebpackPlugin = require('clean-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin')
const SpeedMeasurePlugin = require('speed-measure-webpack-plugin');
const { CheckerPlugin } = require('awesome-typescript-loader');

const outputPath = path.resolve(__dirname, 'wwwroot');
const clientPath = './ClientApp';

module.exports = function (env, argv) {

    const isDevelopmentMode = argv.mode === 'development';

    const smp = new SpeedMeasurePlugin();

    const config = smp.wrap({
        entry: {
            vendor: path.resolve(clientPath, 'vendor.js'),
            app: path.resolve(clientPath, 'app', 'app.tsx')
        },
        devtool: 'inline-source-map',
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
            }),
            new CheckerPlugin()
        ],
        module: {
            rules: [
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
                        loader: 'awesome-typescript-loader',
                        options: {
                            useBabel: true,
                            babelOptions: {
                                babelrc: true
                            },
                            useCache: true
                        }
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