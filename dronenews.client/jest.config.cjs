// eslint-disable-next-line no-undef
module.exports = {
  transform: {
    '^.+\\.tsx?$': 'babel-jest',
  },
 testEnvironment: 'jsdom',
  moduleFileExtensions: ['js', 'jsx', 'ts', 'tsx'],
};
