// eslint-disable-next-line no-undef
module.exports = {
  transform: {
    '^.+\\.tsx?$': 'babel-jest'
  },
  testEnvironment: 'jsdom',
  moduleFileExtensions: ['js', 'jsx', 'ts', 'tsx'],
  moduleNameMapper: {
    '\\.(css|less|scss|sss|styl)$': 'jest-transform-stub',
    '\\.(gif|ttf|eot|svg|png)$': 'jest-transform-stub'
  }
}
;
