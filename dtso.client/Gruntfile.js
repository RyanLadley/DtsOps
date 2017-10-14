//Concatinate all third party libraries into one file.

module.exports = function (grunt) {

  // Project configuration.
  grunt.initConfig({
    pkg: grunt.file.readJSON('package.json'),

      
    concat: {
        options: {
          separator: ';',
        },
        dist: {
            src: ['./src/assets/lib/jquery/jquery-2.2.4.js', './src/assets/lib/ng-flow/*.min.js'],
            dest: './src/assets/lib/workspace/libraries.js'
        }
    },

    uglify: {
      build: {
          src: './src/assets/lib/workspace/libraries.js',
          dest: './src/assets/lib/workspace/libraries.min.js'
      }
    }
  });
    
  grunt.loadNpmTasks('grunt-contrib-concat');
  grunt.loadNpmTasks('grunt-contrib-uglify');

  // Default task(s).
  grunt.registerTask('default', ['concat', 'uglify']);

};